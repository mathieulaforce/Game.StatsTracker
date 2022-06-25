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

    public void GetMatchScoreBoard()
    {
        this.MatchScoreBoard
    }
}