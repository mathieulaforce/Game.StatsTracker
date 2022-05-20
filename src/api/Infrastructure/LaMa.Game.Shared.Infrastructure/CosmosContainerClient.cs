using Microsoft.Azure.Cosmos;

namespace LaMa.Game.Shared.Infrastructure;

public interface ICosmosContainerClient
{
    Container Container { get; }
}

public class CosmosContainerClient: ICosmosContainerClient
{
    private readonly CosmosClient _client;
    private readonly string _databaseName;
    private readonly string _containerName;

    public CosmosContainerClient(CosmosClient client, string databaseName, string containerName)
    {
        _client = client;
        _databaseName = databaseName;
        _containerName = containerName;
    }

    public Container Container => _client.GetContainer(_databaseName, _containerName);
}