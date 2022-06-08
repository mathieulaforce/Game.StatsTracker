using AAO25.Client;
using AAO25.Client.Models;
using LaMa.Game.Shared.Infrastructure;
using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Data.Mappers;
using LaMa.StatsTracking.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace LaMa.StatsTracking.Data;

public interface IServerRepository
{
    Task GetAndUpdateOnlineServers();
    Task<IReadOnlyCollection<GameServer>> Get(bool includeOffline);
    GameSession GetServerSession(string ipAddressWithPort);
}

public class ServerRepository : IServerRepository
{
    private readonly IAAO25Client _client;
    private readonly ICosmosContainerClient _cosmsContainerClient;

    public ServerRepository(IAAO25Client client, ICosmosContainerClient cosmsContainerClient)
    {
        _client = client;
        _cosmsContainerClient = cosmsContainerClient;
    }

    public async Task GetAndUpdateOnlineServers()
    {
        var onlineServers = await _client.GetServers();
        var requestTime = DateTimeOffset.UtcNow;
        var feedIterator = _cosmsContainerClient.Container.GetItemLinqQueryable<GameServerDTO>().ToFeedIterator();
        var knownServers = new List<GameServerDTO>();
        while (feedIterator.HasMoreResults)
        {
            knownServers.AddRange(await feedIterator.ReadNextAsync());
        }

        var newServers = onlineServers.Where(server =>
                knownServers.All(known => known.Ip != server.Ip && known.Name != server.Name))
            .Select(server => server.MapToDto())
            .ToList();

        var offlineServers = knownServers.Where(server =>
            onlineServers.Any(known => known.Ip != server.Ip && known.Name == server.Name)).ToList();

        foreach (var serverDto in offlineServers)
        {
            serverDto.isOnline = false;
            await _cosmsContainerClient.Container.UpsertItemAsync(serverDto); 
        }
             
        knownServers.AddRange(newServers);

        foreach (var gameServerDto in knownServers)
        {
            var onlineServer = onlineServers.First(server => server.Ip == gameServerDto.Ip).MapToDto();
            onlineServer.LastOnline = requestTime;
            onlineServer.isOnline = true; 
            await _cosmsContainerClient.Container.UpsertItemAsync(onlineServer);
        } 
    }

    public async Task<IReadOnlyCollection<GameServer>> Get(bool includeOffline)
    {
        IQueryable<GameServerDTO> query = _cosmsContainerClient.Container.GetItemLinqQueryable<GameServerDTO>();
        if (!includeOffline)
        {
            query = query.Where(server => server.isOnline);
        } 
        var feedIterator = query.ToFeedIterator();
        var servers = new List<GameServerDTO>();
        while (feedIterator.HasMoreResults)
        {
            var result = await feedIterator.ReadNextAsync();
            servers.AddRange(result.Resource);
        }

        return servers.MapToDomain();
    }

    public GameSession GetServerSession(string ipAddressWithPort)
    {
        var gameSession = _client.GetServerDetails(ipAddressWithPort);
        return gameSession;
    }
}