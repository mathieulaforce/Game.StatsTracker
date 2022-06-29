using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.StatsTracking.Data.Mappers;

public static class GameMatchMapper
{
    public static GameMatch MapToDomain(this GameMatchDTO match)
    {
        var serverIp = new IpAddress(match.Ip);
        var domain = new GameMatch(match.Id, serverIp, match.MapName);
        foreach (var matchRoundScore in match.RoundScores)
        {
            domain.RegisterCompletedRound(new RoundInformation(matchRoundScore.RoundNumber,match.TotalRounds,"" ), matchRoundScore.SessionPlayers.MapToDomain(),matchRoundScore.DisconnectedPlayers.MapToDomain());
        }

        domain.SetRoundInformation(new RoundInformation(match.CurrentRound, match.TotalRounds, match.TimeLeft));
        return domain;
    }

    public static GameMatchDTO MapToDTO(this GameMatch match)
    {
        return new GameMatchDTO
        {
            Id = match.Id,
            CurrentRound = match.CurrentRoundInformation.CurrentRound,
            TotalRounds = match.CurrentRoundInformation.TotalRounds,
            TimeLeft = match.CurrentRoundInformation.TimeLeft,
            IsFinished = match.IsFinished,
            MapName = match.MapName,
            Ip = match.ServerIp.ToString(),
            RoundScores = match.MatchScoreBoard.Rounds.Select(r => new RoundScoreDTO
            {
                SessionPlayers = r.SessionPlayers.Select(p => p.MapToDTO()).ToList(),
                DisconnectedPlayers = r.DisconnectedPlayers.Select(p => p.MapToDTO()).ToList(),
                RoundNumber = r.RoundNumber
            }).ToList(), 
            
        };
    }
}