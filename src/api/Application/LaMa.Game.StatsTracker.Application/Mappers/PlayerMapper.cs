using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.Calculators;

namespace LaMa.Game.StatsTracker.Application.Mappers;

public static class PlayerMapper
{
    public static SessionPlayer MapToDomain(this AAO25.Client.Models.Player player)
    {
        var sessionPlayer = new SessionPlayer(player.Name, player.Leader, player.Goal, player.Ping, player.Roe, player.Honor, new PlayerScoreCalculator());
        sessionPlayer.SetAndCalculateKills(player.Enemy, player.Kia);
        return sessionPlayer;
    }
}