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
    Task<IReadOnlyCollection<GameServer>> Get(bool includeOffline, bool trackingOnly);
    Task GetServerSession(string ipAddressWithPort);
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
        var serversToDelete = knownServers.Where(server =>
            onlineServers.Any(known => known.Ip != server.Ip && known.Name == server.Name)).ToList();

        foreach (var serverDto in serversToDelete)
            await _cosmsContainerClient.Container.DeleteItemAsync<GameServerDTO>(serverDto.Ip, new PartitionKey("AA"));

        knownServers.AddRange(newServers);

        foreach (var gameServerDto in knownServers)
        {
            gameServerDto.LastOnline = requestTime;
            await _cosmsContainerClient.Container.UpsertItemAsync(gameServerDto);
        }


        var result = knownServers.MapToDomain();
    }

    public async Task<IReadOnlyCollection<GameServer>> Get(bool includeOffline, bool trackingOnly)
    {
        var servers = await _client.GetServers();
        servers = servers.Where(server =>
        {
            if (trackingOnly)
            {
                return server.NumberOfPlayers > 2;
            }

            return true;
        }).ToList();
        return servers.Select(server => server.MapToDto().MapToDomain()).ToList();
    }

    public GameSession GetServerSession(string ipAddressWithPort)
    {
        var gameSession = _client.GetServerDetails(ipAddressWithPort);
        return gameSession.MapSession();
    }
}