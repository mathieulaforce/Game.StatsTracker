using Azure;
using Azure.Messaging.EventGrid;

namespace LaMa.Game.GameSessionProcessor.Client
{

    public interface IGameSessionProcessorEventPublisher
    {
        Task PublishGameSessionTracking(string ip, int port);
        Task PublishFinalizeTracking(string ip, int port); 
    }
    public class GameSessionProcessorEventPublisher : IGameSessionProcessorEventPublisher
    {

        private readonly EventGridPublisherClient _eventGridPublisherClient;

        public GameSessionProcessorEventPublisher( )
        {
            var uri = new Uri("https://statstracker-event-topic.northeurope-1.eventgrid.azure.net/api/events");
            _eventGridPublisherClient = new EventGridPublisherClient(uri, new AzureKeyCredential("zUa2SBjB83lwoH41BvEB8ka2kWXQ9W4J3ZTjmsA+hRc="));
        }
        public async Task PublishGameSessionTracking(string ip, int port)
        {
            var message =
                new EventGridEvent(
                    "GameSessionProcessor",
                    GameSessionProcessEventTypes.GameSessionTracking,
                    "1.0", new GameSessionProcessorEventMessage
                    {
                        Ip = ip,
                        Port = port
                    });
            var response = await _eventGridPublisherClient.SendEventAsync(message);
        }

        public async Task PublishFinalizeTracking(string ip, int port)
        {
            var message =
                new EventGridEvent(
                    "GameSessionProcessor",
                    GameSessionProcessEventTypes.FinalizeTracking,
                    "1.0", new GameSessionProcessorEventMessage
                    {
                        Ip = ip,
                        Port = port
                    });
            var response = await _eventGridPublisherClient.SendEventAsync(message);
        }
    }
}