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

    public static TrackedPlayer MapToDomain(this PlayerDTO player,  IPlayerScoreCalculator calculator)
    {
        var trackedPlayer = new TrackedPlayer(player.Name, player.Leader, calculator);
        trackedPlayer.SetScore(player.Goal,player.Leader, player.Roe, player.TotalPoints, player.KillPoints, player.KiaPoints);
        trackedPlayer.SetHonours(player.TimesGoldPlayer, player.TimesSilverPlayer, player.TimesBronzePlayer, player.TimesDeadliestPlayer);
        trackedPlayer.SetRecords(player.LargestGoalScore, player.LargestLeaderScore, player.LargestTotalScore, player.LargestKillStreak, player.LargestDeathStreak, player.LowestRoeScore);
        trackedPlayer.SetReconnectsAndRoundsPlayed(player.Reconnects, player.RoundsPlayed);
        return trackedPlayer;
    }

    public static PlayerDTO MapToDto(this TrackedPlayer player)
    {
        return new PlayerDTO
        { 
            Name = player.Name,
            Goal = player.Goal,
            Honor = player.Honor, 
            KillPoints = player.KillPoints,
            KiaPoints = player.KiaPoints,
            Leader = player.Leader,
            Roe = player.Roe,
            RoundsPlayed = player.RoundsPlayed,
            FragRate = player.FragRate,
            Kia = player.Kia,
            Kills = player.Kills,
            LargestDeathStreak = player.LargestDeathStreak,
            LargestLeaderScore = player.LargestLeaderScore,
            LargestTotalScore = player.LargestTotalScore,
            LargestKillStreak = player.LargestKillStreak,
            LargestGoalScore = player.LargestGoalScore,
            LowestRoeScore = player.LowestRoeScore,
            Reconnects = player.Reconnects,
            TimesBronzePlayer = player.TimesBronzePlayer,
            TimesDeadliestPlayer = player.TimesDeadliestPlayer,
            TimesGoldPlayer = player.TimesGoldPlayer,
            TimesSilverPlayer = player.TimesSilverPlayer,
            TotalPoints = player.TotalPoints,
            
        };
    }
    


}