using AAO25.Client;
using AAO25.Client.Models;
using LaMa.Game.Shared.Infrastructure;
using LaMa.StatsTracking.Data.DTO;
using LaMa.StatsTracking.Data.Mappers;
using LaMa.StatsTracking.Domain;
using LaMa.StatsTracking.Domain.Calculators;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace LaMa.StatsTracking.Data;

public interface IPlayerRepository
{  
    Task UpdateScores(List<TrackedPlayer> trackedPlayer); 
}

public class PlayerRepository : IPlayerRepository
{ 
    private readonly ICosmosContainerClient _cosmosContainerClient;
    private readonly IPlayerScoreCalculator _calculator;

    public PlayerRepository(ICosmosContainerClient cosmosContainerClient, IPlayerScoreCalculator calculator)
    {
        _cosmosContainerClient = cosmosContainerClient;
        _calculator = calculator;
    }

    private Container Container => _cosmosContainerClient.GetContainer("Players");
    
    public async Task UpdateScores(List<TrackedPlayer> trackedPlayer)
    { 
        foreach (var player in trackedPlayer)
        {
            var feedIterator = Container.GetItemLinqQueryable<PlayerDTO>()
                .Where(_ => _.Name == player.Name).ToFeedIterator();

            PlayerDTO playerDTO = null;
            while (feedIterator.HasMoreResults)
            {
                var res = await feedIterator.ReadNextAsync();
                playerDTO = res.Resource?.FirstOrDefault();
            }

            if (playerDTO == null)
            {
                playerDTO = new PlayerDTO
                {
                    Name = player.Name,
                    Honor = player.Honor,
                };
            }

            var existingDomainPlayer = playerDTO.MapToDomain();
            existingDomainPlayer.AddMatchScore(player);
            var dto = existingDomainPlayer.MapToDto();
            var response = await Container.UpsertItemAsync(dto);
        }
    }
}