namespace LaMa.StatsTracking.Domain
{
    public interface IPlayer
    {
        string Name { get; }
        int Leader { get; }
        int Goal { get; }
        int Ping { get; }
        int Roe { get; }
        int KiaPoints { get; }
        int KillPoints { get; }
        int Honor { get; }
    }
}