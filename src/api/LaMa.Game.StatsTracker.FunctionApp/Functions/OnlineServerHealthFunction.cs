using System;
using System.Net.Http;
using System.Threading.Tasks;
using AAO25.Client;
using LaMa.StatsTracking.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LaMa.Game.StatsTracker.FunctionApp.Functions
{
  public class OnlineServerHealthFunction
  {
      private readonly IServerRepository _serverRepository; 
    public OnlineServerHealthFunction(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository; 
    }

    [FunctionName("OnlineServerHealthFunction")]
    public async Task Run([TimerTrigger("0/30 * * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
    {
      log.LogInformation($"Fetching server list on {DateTime.UtcNow}");
      using var client = new HttpClient();
      await _serverRepository.GetAndUpdateOnlineServers(); 
    }
  }
}
