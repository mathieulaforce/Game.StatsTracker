using AAO25.Client;
using LaMa.Game.GameSessionProcessor.Client;
using LaMa.Game.StatsTracker.Application.Mappers;
using LaMa.StatsTracking.Data;
using LaMa.StatsTracking.Domain;

namespace LaMa.Game.StatsTracker.Application;

public interface IServerApplicationService
{
    Task ContactAndUpdateServers();
}

public class ServerApplicationService : IServerApplicationService
{
    private readonly IAAO25Client _aao25Client;
    private readonly IServerRepository _serverRepository;
    private readonly IGameSessionProcessorEventPublisher _gameSessionProcessorEventPublisher;

    public ServerApplicationService(IAAO25Client aao25Client, IServerRepository serverRepository, IGameSessionProcessorEventPublisher gameSessionProcessorEventPublisher)
    {
        _aao25Client = aao25Client;
        _serverRepository = serverRepository;
        _gameSessionProcessorEventPublisher = gameSessionProcessorEventPublisher;
    }
    public async Task ContactAndUpdateServers()
    {
        var requestTime = DateTimeOffset.UtcNow;
        var onlineServers = (await _aao25Client.GetServers()).Select(server => server.MapToGameServer(requestTime));
        var knownServers =await _serverRepository.GetServers();
         
        var offlineServers = knownServers.Where(knownServer => onlineServers.All(knownServer.IsSameServer)).ToList();

        foreach (var offlineServer in offlineServers.Where(server => server.IsOnline))
        {
            if (offlineServer.HasPlayers())
            {
                await _gameSessionProcessorEventPublisher.PublishFinalizeMatch(offlineServer.IpAddress.Ip, offlineServer.IpAddress.Port);
            }
            offlineServer.SetOffline();
            await _serverRepository.InsertOrUpdate(offlineServer);
        }

        foreach (var onlineServer in onlineServers)
        { 
            await _serverRepository.InsertOrUpdate(onlineServer);
            if (onlineServer.HasPlayers())
            { 
                await _gameSessionProcessorEventPublisher.PublishGameSessionTracking(onlineServer.IpAddress.Ip, onlineServer.IpAddress.Port);
            }
        } 
    }
}