using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.Calculators;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Data.Mappers;

public static class PlayerMapper
{
    public static SessionPlayerDTO MapToDTO(this SessionPlayer player)
    {
        return new SessionPlayerDTO
        {
            Goal = player.Goal,
            Honor = player.Honor,
            KillPoints = player.KillPoints,
            KiaPoints = player.KiaPoints,
            Leader = player.Leader,
            Roe = player.Roe,
            Name = player.Name,
            Ping = player.Ping,

        };
    }

    public static List<SessionPlayer> MapToDomain(this List<SessionPlayerDTO> players)
    {
        return players.Select(player => player.MapToDomain()).ToList();
    }

    public static SessionPlayer MapToDomain(this SessionPlayerDTO player)
    {
        var sessionPlayer = new SessionPlayer(player.Name, player.Leader, player.Goal, player.Ping, player.Roe, player.Honor, new PlayerScoreCalculator());
        sessionPlayer.SetAndCalculateKills( player.KillPoints, player.KiaPoints);
        return sessionPlayer;
    }


}