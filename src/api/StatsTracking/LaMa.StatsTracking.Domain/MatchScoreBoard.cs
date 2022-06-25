namespace LaMa.StatsTracking.Domain;

public class MatchScoreBoard
{
    public MatchScoreBoard()
    {
        Rounds = new List<RoundScore>();
    }

    public void StartNewRound(int roundNumber)
    {
        Rounds.Add(new RoundScore(roundNumber));
    }

    public List<RoundScore> Rounds { get; internal set; }

    public void UpdateRound(List<SessionPlayer> players)
    {
        var lastRound = Rounds.OrderByDescending(round => round.RoundNumber).First();
        lastRound.UpdateSession(players);
    }
}