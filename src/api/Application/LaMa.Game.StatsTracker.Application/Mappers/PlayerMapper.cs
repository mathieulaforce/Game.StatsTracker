using LaMa.StatsTracking.Domain;

namespace LaMa.Game.StatsTracker.Application.Mappers;

public static class PlayerMapper
{
    public static SessionPlayer MapToDomain(this AAO25.Client.Models.Player player)
    {
        return new SessionPlayer(player.Name, player.Leader, player.Goal, player.Ping, player.Roe, player.Kia, player.Enemy,
            player.Honor);
    }
}