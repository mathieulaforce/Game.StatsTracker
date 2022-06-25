using AAO25.Client;
using LaMa.Game.GameSessionProcessor.Client;
using LaMa.Game.StatsTracker.Application.Mappers;
using LaMa.StatsTracking.Data;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.Game.StatsTracker.Application;

public interface IGameSessionApplicationService
{ 

    Task HandleGameSessionSnapshot(string ip, int port);

    Task FinalizeMatch(string ip, int port);
}

public class GameSessionApplicationService : IGameSessionApplicationService
{
    private readonly IAAO25Client _aao25Client;
    private readonly IGameMatchRepository _gameMatchRepository; 
    private readonly IGameSessionProcessorEventPublisher _gameSessionProcessorEventPublisher;

    public GameSessionApplicationService(IAAO25Client aao25Client, IGameMatchRepository gameMatchRepository, IGameSessionProcessorEventPublisher gameSessionProcessorEventPublisher)
    {
        _aao25Client = aao25Client;
        _gameMatchRepository = gameMatchRepository;
        _gameSessionProcessorEventPublisher = gameSessionProcessorEventPublisher;
    } 

    public async Task HandleGameSessionSnapshot(string ip, int port)
    {
        var ipAddress = new IpAddress(ip, port);
        var gameSessionSnapShot = _aao25Client.GetServerDetails(ipAddress.ToString()).MapToLiveGameSession();
        var serverMatches = _gameMatchRepository.GetServerMatches(ipAddress);
        serverMatches.RegisterGameSession(gameSessionSnapShot);
        await _gameMatchRepository.UpdateGameMatches(serverMatches);
        foreach (var match in serverMatches.GetGameMatches().Where(_=>_.IsFinished))
        {
            await _gameSessionProcessorEventPublisher.PublishFinalizeMatch(match.ServerIp.Ip, match.ServerIp.Port);
        }

    }

    public async Task FinalizeMatch(string ip, int port)
    {
        var ipAddress = new IpAddress(ip, port);
        var serverMatches =await _gameMatchRepository.GetFinishedServerMatches(ipAddress);

        foreach (var serverMatch in serverMatches)
        {
            var scoreboard = serverMatch.GetFinalizedScoreboard();
        }

    }
}