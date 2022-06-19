using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Data.Mappers;

internal static class GameServerMappers
{
    internal static List<GameServer> MapToDomain(this List<GameServerDTO> servers)
    {
        return servers.Select(MapToDomain).ToList();
    }

    internal static GameServer MapToDomain(this GameServerDTO server)
    {
        var ipAddress = new IpAddress(server.Ip);
        var gameServer = new GameServer(server.Name,
            ipAddress,
            server.Mapname,
            server.CountryIso2,
            server.Ping,
            server.NumberOfPlayers, 
            server.MaxPlayers,
            server.Version,
            server.PasswordProtected);
        gameServer.SetOnlineStatusByTimestamp( server.LastOnline);
        return gameServer;
    }

    internal static GameServerDTO MapToDto(this GameServer server)
    {
        return new GameServerDTO
        {
            isOnline = server.IsOnline,
            NumberOfPlayers = server.NumberOfPlayers,
            MaxPlayers = server.MaxPlayers,
            Version = server.Version,
            LastOnline = server.LastOnline,
            Ip = server.IpAddress.ToString(),
            CountryIso2 = server.CountryIso2,
            Ping = server.Ping,
            Game = "AAO2.5",
            Mapname = server.MapName,
            Name = server.Name,
            PasswordProtected = server.PasswordProtected
        };
    }
}