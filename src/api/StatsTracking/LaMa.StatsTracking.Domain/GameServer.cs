namespace LaMa.StatsTracking.Domain;

public class GameServer
{
    public string Name { get; init; }
    public string Ip { get; init; }
    public string Mapname { get; init; }
    public string CountryIso2 { get; init; }
    public int Ping { get; init; }
    public int NumberOfPlayers { get; init; }
    public int MaxPlayers { get; init; }
    public string Version { get; init; }
    public DateTimeOffset LastOnline { get; init; }
    public bool IsTracking => NumberOfPlayers > 3;
    public bool IsOnline { get; init; }
}