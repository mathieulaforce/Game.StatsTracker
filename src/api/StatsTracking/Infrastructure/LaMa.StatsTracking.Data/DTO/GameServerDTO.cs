namespace LaMa.StatsTracking.Data.DTO;

internal class GameServerDTO
{
    public string Id => Ip;
    public string Name { get; init; }
      public string Ip { get; init; }
    public string Mapname { get; init; }
    public string CountryIso2 { get; init; }
    public int Ping { get; init; }
    public int NumberOfPlayers { get; init; }
    public int MaxPlayers { get; init; }
    public string Version { get; init; }
    public DateTimeOffset LastOnline { get; set; }
    public bool isOnline{ get; set; }
    public string Game { get; init; }
}

  