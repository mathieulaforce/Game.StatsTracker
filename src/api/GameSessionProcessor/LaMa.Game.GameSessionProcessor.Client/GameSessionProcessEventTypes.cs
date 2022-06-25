namespace LaMa.Game.GameSessionProcessor.Client;

public static class GameSessionProcessEventTypes
{
    public static readonly string GameSessionTracking = nameof(GameSessionTracking);
    public static readonly string FinalizeMatch = nameof(FinalizeMatch);

    public static bool IsEventType(string value)
    {
        return new []
        {
            GameSessionTracking, 
            FinalizeMatch
        }.Contains(value);
    }
}