using System;
using AAO25.Client;
using LaMa.Game.GameSessionProcessor.Client;
using LaMa.Game.Shared.Infrastructure;
using LaMa.Game.StatsTracker.Application;
using LaMa.Game.StatsTracker.FunctionApp;
using LaMa.StatsTracking.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

[assembly: FunctionsStartup(typeof(Startup))]
namespace LaMa.Game.StatsTracker.FunctionApp;

public class Startup: FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        var authKey = config.GetValue<string>("COSMOS_DB_AUTH_KEY");
        var endpoint = config.GetValue<string>("COSMOS_ENDPOINT");
        
        builder.Services.AddSingleton(config);
        
        builder.Services.AddTransient<IServerRepository, ServerRepository>();
        builder.Services.AddTransient<IGameMatchRepository, GameMatchRepository>();
        builder.Services.AddTransient<IServerApplicationService, ServerApplicationService>();
        builder.Services.AddTransient<IGameSessionProcessorEventPublisher, GameSessionProcessorEventPublisher>();
        builder.Services.AddTransient<IGameSessionApplicationService, GameSessionApplicationService>();

        //Move to correct place
        builder.Services.AddHttpClient<IAAO25Client, AAO25Client>(client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("SERVER_LIST_ENDPOINT"));
        }); 

        builder.Services.AddSingleton<CosmosClient>(_ => new CosmosClientBuilder(endpoint, authKey)
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .WithBulkExecution(true)
            .Build());
        builder.Services.AddSingleton<ICosmosContainerClient, CosmosContainerClient>(provider => new CosmosContainerClient(provider.GetService<CosmosClient>(), "lama-tracker"));

        using (var client = new CosmosClient(endpoint, authKey))
        {
            var db = client.CreateDatabaseIfNotExistsAsync("lama-tracker").GetAwaiter().GetResult();
            db.Database.CreateContainerIfNotExistsAsync("GameServer", "/game").GetAwaiter().GetResult();
            //db.Database.CreateContainerIfNotExistsAsync("GameMatch", "/game").GetAwaiter().GetResult();
        }
        //end move
    }
}
