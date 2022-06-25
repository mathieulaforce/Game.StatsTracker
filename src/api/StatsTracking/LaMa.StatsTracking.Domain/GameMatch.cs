using LaMa.StatsTracking.Domain.Calculators;
using LaMa.StatsTracking.Domain.Exceptions;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Domain;

public class GameMatch
{
    public GameMatch(string id, IpAddress serverIp, string mapName)
    {
        Id = id;
        ServerIp = serverIp;
        MapName = mapName; 
        CurrentRoundInformation = RoundInformation.Empty;
        MatchScoreBoard = new MatchScoreBoard();
    }

    public string Id { get; }
    public IpAddress ServerIp { get; }
    public string MapName { get; }
    public RoundInformation CurrentRoundInformation { get; private set; }

    public MatchScoreBoard MatchScoreBoard { get; private set; }

    public bool IsFinished { get; private set; }

    public bool IsValidSessionForMatch(LiveGameSession liveGameSession)
    {
        return ServerIp == liveGameSession.ServerIp &&
               MapName == liveGameSession.Details.MapName &&
               (RoundInformation.IsEmpty(CurrentRoundInformation) || CurrentRoundInformation.CurrentRound <= liveGameSession.Details.CurrentRound);
    }

    public void RegisterCompletedRound(RoundInformation roundInformation, List<SessionPlayer> players, List<SessionPlayer> disconnectedPlayers)
    {
        CurrentRoundInformation =roundInformation;
        var round = new RoundScore(roundInformation.CurrentRound);
        round.SessionPlayers = players;
        round.DisconnectedPlayers = disconnectedPlayers; 
        MatchScoreBoard.Rounds.Add(round);
    }
    public void RegisterNewRound(RoundInformation roundInformation, List<SessionPlayer> players)
    {
        if (RoundInformation.IsEmpty(CurrentRoundInformation))
        {
            CurrentRoundInformation = roundInformation;
            MatchScoreBoard.StartNewRound(roundInformation.CurrentRound);
        }

        if (CurrentRoundInformation.CurrentRound > roundInformation.CurrentRound)
        {
            throw new InvalidRoundRegistration();
        }

        if (CurrentRoundInformation.CurrentRound == roundInformation.CurrentRound)
        {
            MatchScoreBoard.UpdateRound(players);
            CurrentRoundInformation = roundInformation;
        }
        else
        {
            MatchScoreBoard.StartNewRound(roundInformation.CurrentRound);
            MatchScoreBoard.UpdateRound(players);
            CurrentRoundInformation = roundInformation;
        }

    }

    public void MarkAsFinished()
    {
        IsFinished = true;
    }

    public List<TrackedPlayer> GetFinalizedScoreboard()
    {
        var disconnectedPlayers = new HashSet<string>();
        var scoreOfPlayer = new Dictionary<string, TrackedPlayer>();

        var calculator = new PlayerScoreCalculator();
        var reconnectedPlayerScores = new Dictionary<string, TrackedPlayer>();
        foreach (var roundScore in MatchScoreBoard.Rounds)
        {
            foreach (var roundPlayer in roundScore.SessionPlayers)
            { 
                if (IsNewPlayer(roundPlayer.Name , scoreOfPlayer,  disconnectedPlayers))
                {
                    HandleNewPlayer(roundPlayer, scoreOfPlayer, calculator);
                }
                else if (IsReconnectingPlayer(roundPlayer.Name, scoreOfPlayer, disconnectedPlayers))
                {
                    HandleReconnectingPlayer(scoreOfPlayer, roundPlayer, reconnectedPlayerScores, disconnectedPlayers, calculator);
                }
                else
                {
                    HandlePlayerWhoPlaysNewRound(scoreOfPlayer, roundPlayer);
                }
            }
        }

        var reconnectedPlayers = reconnectedPlayerScores.Select(_ => _.Key).ToHashSet();
        var playerScores = scoreOfPlayer.Values.Concat(reconnectedPlayerScores.Values).ToList();
        foreach (var player in playerScores)
        {
            player.CalculateAllScores(calculator);
        }

        GiveHonorsToBestPlayers(playerScores, reconnectedPlayers);
        return playerScores;
    }

    private void GiveHonorsToBestPlayers(List<TrackedPlayer> playerScores, HashSet<string> reconnectedPlayers)
    {
        var topThree = playerScores.OrderByDescending(_=>_.TotalPoints).Where(_=>!reconnectedPlayers.Contains(_.Name)).Take(3).ToList();
        for (var i = 0; i < topThree.Count; i++)
        {
            if (i == 0)
            {
                topThree[i].GiveGoldMedal();
            }
            if (i == 1)
            {
                topThree[i].GiveSilverMedal();
            }
            if (i == 2)
            {
                topThree[i].GiveBronzeMedal();
            }
        }

        playerScores.OrderByDescending(_ => _.Kills).First().SetDeadliestPlayer();

    }

    private static void HandlePlayerWhoPlaysNewRound(Dictionary<string, TrackedPlayer> scoreOfPlayer, SessionPlayer roundPlayer )
    {
        var trackedPlayer = scoreOfPlayer[roundPlayer.Name];
        trackedPlayer.AddRoundPlayed(roundPlayer.Goal,
            roundPlayer.Leader,
            roundPlayer.Roe,
            roundPlayer.KillPoints,
            roundPlayer.KiaPoints);
    }

    private static void HandleReconnectingPlayer(Dictionary<string, TrackedPlayer> scoreOfPlayer, SessionPlayer roundPlayer,
        Dictionary<string, TrackedPlayer> scoresToSave, HashSet<string> disconnectedPlayers, IPlayerScoreCalculator calculator)
    {
        var lastKnownScore = scoreOfPlayer[roundPlayer.Name];

        if (!scoresToSave.ContainsKey(roundPlayer.Name))
        {
            scoresToSave.Add(roundPlayer.Name, new TrackedPlayer(roundPlayer.Name, roundPlayer.Honor, calculator));
        }

        var alreadySavedScore = scoresToSave[roundPlayer.Name];
        alreadySavedScore.AddBasicScore(lastKnownScore);
        alreadySavedScore.AddReconnect();

        var trackedPlayer = new TrackedPlayer(roundPlayer.Name, roundPlayer.Honor, calculator);
        trackedPlayer.AddRoundPlayed(roundPlayer.Goal,
            roundPlayer.Leader,
            roundPlayer.Roe,
            roundPlayer.KillPoints,
            roundPlayer.KiaPoints);

        scoreOfPlayer[trackedPlayer.Name] = trackedPlayer;
        disconnectedPlayers.Remove(trackedPlayer.Name);
    }

    private static void HandleNewPlayer(SessionPlayer roundPlayer, Dictionary<string, TrackedPlayer> scoreOfPlayer, IPlayerScoreCalculator calculator)
    {
        var trackedPlayer = new TrackedPlayer(roundPlayer.Name, roundPlayer.Honor, calculator);
        trackedPlayer.AddRoundPlayed(roundPlayer.Goal,
            roundPlayer.Leader,
            roundPlayer.Roe,
            roundPlayer.KillPoints,
            roundPlayer.KiaPoints);
        scoreOfPlayer.Add(trackedPlayer.Name, trackedPlayer);
    }

    private static bool IsNewPlayer(string playerName, Dictionary<string, TrackedPlayer> scoreOfPlayer, HashSet<string> disconnectedPlayers)
    {
        return !scoreOfPlayer.ContainsKey(playerName) && !disconnectedPlayers.Contains(playerName);
    }
    private static bool IsReconnectingPlayer(string playerName, Dictionary<string, TrackedPlayer> scoreOfPlayer, HashSet<string> disconnectedPlayers)
    {
        return scoreOfPlayer.ContainsKey(playerName) && disconnectedPlayers.Contains(playerName);
    }
}