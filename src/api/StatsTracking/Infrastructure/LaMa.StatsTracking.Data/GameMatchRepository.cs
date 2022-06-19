﻿using AAO25.Client;
using AAO25.Client.Models;
using LaMa.Game.Shared.Infrastructure;
using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Data.Mappers;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.ValueObjects;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace LaMa.StatsTracking.Data;

public interface IGameMatchRepository
{
    ServerGameMatches GetServerMatches(IpAddress ipAddress);

    Task UpdateGameMatches(ServerGameMatches serverMatches);
}

public class GameMatchRepository : IGameMatchRepository
{
    private readonly ICosmosContainerClient _cosmosContainerClient;

    public GameMatchRepository(ICosmosContainerClient cosmosContainerClient)
    { 
        _cosmosContainerClient = cosmosContainerClient;
    }

    private Container Container => _cosmosContainerClient.GetContainer("GameMatch");

    public ServerGameMatches GetServerMatches(IpAddress ipAddress)
    {
        var gameMatch = Container.GetItemLinqQueryable<GameMatchDTO>(true)
            .Where(gameMatch => gameMatch.ServerIp == ipAddress.ToString())
            .AsEnumerable()
            .FirstOrDefault();
         
        var match = gameMatch?.MapToDomain();
    
        if (match == null)
        {
            return new ServerGameMatches();
        }

        return new ServerGameMatches(new[] { match });
    }

    public async Task UpdateGameMatches(ServerGameMatches serverMatches)
    {
        foreach (var serverGameMatch in serverMatches.GetGameMatches())
        {
            var dto = serverGameMatch.MapToDTO();
            var response = await Container.UpsertItemAsync(dto); 
        }
        
    }
}