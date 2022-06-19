namespace LaMa.StatsTracking.Domain;

public class RoundInformation
{
    public RoundInformation(int currentRound, int totalRounds, string timeLeft)
    { 
        CurrentRound = currentRound;
        TotalRounds = totalRounds; 
        TimeLeft = timeLeft;
    }

    public int CurrentRound { get; }
    public int TotalRounds { get; } 
    public string TimeLeft { get; }

    public bool IsFinalRound()
    {
        return CurrentRound == TotalRounds;
    }

    public static bool IsEmpty(RoundInformation roundInformation)
    {
        return roundInformation.CurrentRound == 0 && roundInformation.TotalRounds == 0 && string.IsNullOrWhiteSpace(roundInformation.TimeLeft);
    }

    public static RoundInformation Empty => new(0, 0, string.Empty);
}