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

    Task FinilazeGameSession(string ip, int port);
}

public class GameSessionApplicationService : IGameSessionApplicationService
{
    private readonly IAAO25Client _aao25Client;
    private readonly IGameMatchRepository _gameMatchRepository;


    public GameSessionApplicationService(IAAO25Client aao25Client, IGameMatchRepository gameMatchRepository)
    {
        _aao25Client = aao25Client;
        _gameMatchRepository = gameMatchRepository;
    } 

    public async Task HandleGameSessionSnapshot(string ip, int port)
    {
        var ipAddress = new IpAddress(ip, port);
        var gameSessionSnapShot = _aao25Client.GetServerDetails(ipAddress.ToString()).MapToLiveGameSession();
        var serverMatches = _gameMatchRepository.GetServerMatches(ipAddress);
        serverMatches.RegisterGameSession(gameSessionSnapShot);
        await _gameMatchRepository.UpdateGameMatches(serverMatches); 
    }

    public Task FinilazeGameSession(string ip, int port)
    {
        throw new NotImplementedException();
    }
}