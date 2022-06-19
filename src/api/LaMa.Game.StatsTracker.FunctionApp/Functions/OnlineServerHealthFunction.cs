using System;
using System.Net.Http;
using System.Threading.Tasks; 
using LaMa.Game.StatsTracker.Application; 
using Microsoft.Azure.WebJobs; 
using Microsoft.Extensions.Logging;

namespace LaMa.Game.StatsTracker.FunctionApp.Functions
{
  public class OnlineServerHealthFunction
  {
      private readonly IServerApplicationService _serverApplicationService;
      
    public OnlineServerHealthFunction(IServerApplicationService serverApplicationService)
    {
        _serverApplicationService = serverApplicationService;
    }

    [FunctionName(nameof(OnlineServerHealthFunction))]
    public async Task Run([TimerTrigger("0/30 * * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
    { 
      log.LogInformation($"Fetching server list on {DateTime.UtcNow}");
      using var client = new HttpClient();
      await _serverApplicationService.ContactAndUpdateServers(); 
    }
  }
}
