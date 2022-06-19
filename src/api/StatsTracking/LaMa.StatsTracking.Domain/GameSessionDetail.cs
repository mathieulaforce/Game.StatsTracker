namespace LaMa.StatsTracking.Domain;

public class GameSessionDetail
{
    public GameSessionDetail(int currentRound, int totalRound, string mapName, string timeLeft)
    {
        CurrentRound = currentRound;
        TotalRound = totalRound;
        MapName = mapName;
        TimeLeft = timeLeft;
    }

    public int CurrentRound { get; }
    public int TotalRound { get; }
    public string MapName { get; }
    public string TimeLeft { get;}

    public bool IsFinalRound()
    {
        return CurrentRound == TotalRound;
    }
}