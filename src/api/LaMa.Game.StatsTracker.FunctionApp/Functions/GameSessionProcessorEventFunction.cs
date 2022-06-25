// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=GameSessionProcessorEventFunction
using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using LaMa.Game.GameSessionProcessor.Client;
using System.Text.Json;
using LaMa.Game.StatsTracker.Application;
using LaMa.StatsTracking.Domain.ValueObjects;

namespace LaMa.Game.StatsTracker.FunctionApp.Functions
{
    public class GameSessionProcessorEventFunction
    {
        private readonly IGameSessionApplicationService _gameSessionApplicationService;

        public GameSessionProcessorEventFunction(IGameSessionApplicationService gameSessionApplicationService)
        {
            _gameSessionApplicationService = gameSessionApplicationService;
        }

        [FunctionName(nameof(GameSessionProcessorEventFunction))]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        { 
            var processEventData = GetEventGridProcessData(eventGridEvent);
            if (!processEventData.CanProcess)
            {
                log.LogInformation($"could not process event: ${eventGridEvent.EventType}");
                return;
            }

            if (GameSessionProcessEventTypes.GameSessionTracking == eventGridEvent.EventType)
            {
                await _gameSessionApplicationService.HandleGameSessionSnapshot(processEventData.Data.Ip, processEventData.Data.Port);
            }
            if (GameSessionProcessEventTypes.FinalizeMatch == eventGridEvent.EventType)
            {
                await _gameSessionApplicationService.FinalizeMatch(processEventData.Data.Ip, processEventData.Data.Port);
            }
        }

        private (bool CanProcess,string EventType, GameSessionProcessorEventMessage Data ) GetEventGridProcessData(EventGridEvent eventGridEvent)
        {
            if (GameSessionProcessEventTypes.IsEventType(eventGridEvent.EventType))
            {
                var eventMessage = eventGridEvent.Data.ToObjectFromJson<GameSessionProcessorEventMessage>(); 
                return new(true, eventGridEvent.EventType, eventMessage);
            }
            return new(false, eventGridEvent.EventType, null); 
        }
    }
}
