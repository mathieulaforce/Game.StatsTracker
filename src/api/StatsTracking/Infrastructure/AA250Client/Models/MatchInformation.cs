namespace AAO25.Client.Models;

public class MatchInformation
{
    public MatchInformation(string mapName, string timeLeft, int currentRound, int totalRounds)
    {
        MapName = mapName;
        TimeLeft = timeLeft;
        CurrentRound = currentRound;
        TotalRounds = totalRounds;
    }

    public string MapName { get; }
    public string TimeLeft { get; }
    public int CurrentRound { get; }
    public int TotalRounds { get; }


    public bool IsLastRound()
    {
        return CurrentRound == TotalRounds;
    }
}