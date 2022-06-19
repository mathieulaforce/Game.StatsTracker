using AAO25.Client.Models;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.Builders;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.Game.StatsTracker.Application.Mappers;

public static class GameSessionMapper
{
    public static LiveGameSession MapToLiveGameSession(this GameSession gameSession)
    {
        var serverIp = new IpAddress(gameSession.Server.IpAddress);
        var matchInformation = gameSession.MatchInformation;
        var players = gameSession.ScoreBoard.OnlinePlayers.Select(player => player.MapToDomain());
        return LiveGameSessionBuilder.ForServer(serverIp)
            .OnMap(matchInformation.MapName)
            .OnRound(matchInformation.CurrentRound, matchInformation.TotalRounds, matchInformation.TimeLeft)
            .AddPlayers(players)
            .Build(); 
    }
}