using AAO25.Client.Models;
using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Domain;

namespace LaMa.StatsTracking.Data.Mappers;

internal static class GameServerMappers
{
    internal static GameServerDTO MapToDto(this AAOServer aaoServer)
    {
        return new GameServerDTO
        {
            Ip = aaoServer.Ip,
            CountryIso2 = aaoServer.CountryIso2,
            LastOnline = aaoServer.LastOnlineSince,
            isOnline = aaoServer.IsOnline,
            Mapname = aaoServer.Mapname,
            MaxPlayers = aaoServer.MaxPlayers,
            Name = aaoServer.Name,
            NumberOfPlayers = aaoServer.NumberOfPlayers,
            Ping = aaoServer.Ping,
            Version = aaoServer.Version,
            Game = "AA250"
        };
    }

    internal static List<GameServer> MapToDomain(this List<GameServerDTO> servers)
    {
        return servers.Select(MapToDomain).ToList();
    }

    internal static GameServer MapToDomain(this GameServerDTO server)
    {
        return new GameServer
        {
            Ip = server.Ip,
            CountryIso2 = server.CountryIso2,
            LastOnline = server.LastOnline,
            IsOnline = server.isOnline,
            Mapname = server.Mapname,
            MaxPlayers = server.MaxPlayers,
            Name = server.Name,
            NumberOfPlayers = server.NumberOfPlayers,
            Ping = server.Ping,
            Version = server.Version, 
        };
    }

}