using System;
using AAO25.Client;
using LaMa.Game.Shared.Infrastructure;
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

        builder.Services.AddSingleton(config);
        
        builder.Services.AddTransient<IServerRepository, ServerRepository>();

        //Move to correct place
        builder.Services.AddHttpClient<IAAO25Client, AAO25Client>(client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("SERVER_LIST_ENDPOINT"));
        });
        builder.Services.AddHttpClient<IAAO25Client, AAO25Client>(client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("SERVER_LIST_ENDPOINT"));
        });

        builder.Services.AddScoped<CosmosClient>(_ => new CosmosClientBuilder("https://localhost:8081/", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==")
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .WithBulkExecution(true)
            .Build());
        builder.Services.AddScoped<ICosmosContainerClient, CosmosContainerClient>(provider => new CosmosContainerClient(provider.GetService<CosmosClient>(),"GameStatsTracker", "GameServer"));
       
        //end move
    }
}
