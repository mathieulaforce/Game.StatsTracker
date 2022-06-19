using Microsoft.Azure.Cosmos;

namespace LaMa.Game.Shared.Infrastructure;

public interface ICosmosContainerClient
{
    Container GetContainer(string containerName);
}

public class CosmosContainerClient: ICosmosContainerClient
{
    private readonly CosmosClient _client;
    private readonly string _databaseName;

    public CosmosContainerClient(CosmosClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
    }
    

    public Container GetContainer(string containerName)
    {
        return _client.GetContainer(_databaseName, containerName);
    }
}