namespace LaMa.Game.GameSessionProcessor.Client;

public static class GameSessionProcessEventTypes
{
    public static readonly string GameSessionTracking = nameof(GameSessionTracking);
    public static readonly string FinalizeTracking = nameof(FinalizeTracking);

    public static bool IsEventType(string value)
    {
        return new []
        {
            GameSessionTracking, 
            FinalizeTracking
        }.Contains(value);
    }
}