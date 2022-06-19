using AAO25.Client.Models;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.Game.StatsTracker.Application.Mappers;

public static class GameServerMapper
{
    public static GameServer MapToGameServer(this AAOServer aaoServer, DateTimeOffset lastOnlineSince)
    {
        var gameServer = new GameServer(aaoServer.Name,
            new IpAddress(aaoServer.Ip), aaoServer.MapName, aaoServer.CountryIso2, aaoServer.Ping,
            aaoServer.NumberOfPlayers, aaoServer.MaxPlayers, aaoServer.Version, aaoServer.PasswordProtected);
        gameServer.SetOnlineStatusByTimestamp(lastOnlineSince);
        return gameServer;
    }
}