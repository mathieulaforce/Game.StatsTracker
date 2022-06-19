using LaMa.StatsTracking.Domain.Exceptions;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Domain;

public class GameServer
{
    public GameServer(string name, IpAddress ipAddress, string mapName, string countryIso2, int ping,
        int numberOfPlayers, int maxPlayers, string version, bool passwordProtected)
    {
        Name = name;
        IpAddress = ipAddress;
        MapName = mapName;
        CountryIso2 = countryIso2;
        Ping = ping;
        NumberOfPlayers = numberOfPlayers;
        MaxPlayers = maxPlayers;
        Version = version;
        PasswordProtected = passwordProtected;
    }

    public string Name { get; private set; }
    public IpAddress IpAddress { get; private set; }
    public string MapName { get; private set; }
    public string CountryIso2 { get; private set; }
    public int Ping { get; private set; }
    public int NumberOfPlayers { get; private set; }
    public int MaxPlayers { get; private set; }
    public string Version { get; private set; }
    public bool PasswordProtected { get; }
    public DateTimeOffset LastOnline { get; private set; }
    public bool IsTracking => NumberOfPlayers > 3;
    public bool IsOnline { get; private set; }

    public void SetOffline()
    {
        if (IsOnline)
        {
            IsOnline = false;
            NumberOfPlayers = 0;
        }
    }
      
    public void SetOnlineStatusByTimestamp(DateTimeOffset lastOnline)
    {
        var timeDiff = DateTimeOffset.UtcNow - lastOnline;
        IsOnline = (timeDiff.TotalSeconds < TimeSpan.FromMinutes(5).TotalSeconds);
        LastOnline = lastOnline;
    }

    public bool IsSameServer(GameServer serverToCompare)
    {
        return IpAddress == serverToCompare.IpAddress && Name == serverToCompare.Name;
    }

    public bool HasPlayers()
    {
        return NumberOfPlayers > 0;
    }
}