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
    GameSession GetServerSession(string ipAddressWithPort); 
    Task<IReadOnlyCollection<GameServer>> GetServers(); 
    Task<GameServer> InsertOrUpdate(GameServer server);
}

public class ServerRepository : IServerRepository
{
    private readonly IAAO25Client _client;
    private readonly ICosmosContainerClient _cosmosContainerClient;

    public ServerRepository(IAAO25Client client, ICosmosContainerClient cosmosContainerClient)
    {
        _client = client;
        _cosmosContainerClient = cosmosContainerClient;
    }

    private Container Container => _cosmosContainerClient.GetContainer("GameServer");
    public GameSession GetServerSession(string ipAddressWithPort)
    {
        var gameSession = _client.GetServerDetails(ipAddressWithPort);
        return gameSession;
    }

    public async Task<IReadOnlyCollection<GameServer>> GetServers()
    {
        var feedIterator = Container.GetItemLinqQueryable<GameServerDTO>().ToFeedIterator();
        var servers = new List<GameServerDTO>();
        while (feedIterator.HasMoreResults)
        {
            servers.AddRange(await feedIterator.ReadNextAsync());
        }

        return servers.MapToDomain();
    }

    public async Task<GameServer> InsertOrUpdate(GameServer server)
    {
        if (server == null)
        {
            throw new ArgumentNullException(nameof(server));
        }
        var response = await Container.UpsertItemAsync(server.MapToDto());
        return response.Resource.MapToDomain();
    }
}