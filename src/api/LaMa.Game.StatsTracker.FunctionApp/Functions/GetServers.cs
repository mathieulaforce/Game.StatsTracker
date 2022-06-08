using System.IO;
using System.Net;
using System.Threading.Tasks;
using LaMa.StatsTracking.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LaMa.Game.StatsTracker.FunctionApp.Functions
{
    public class GetServers
    {
        private readonly ILogger<GetServers> _logger;
        private readonly IServerRepository _serverRepository;

        public GetServers(ILogger<GetServers> log, IServerRepository serverRepository)
        {
            _logger = log;
            _serverRepository = serverRepository;
        }

        [FunctionName("GetServers")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "includeOffline", In = ParameterLocation.Query, Required = false, Type = typeof(bool))] 
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Servers")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            bool.TryParse(req.Query["includeOffline"], out var includeOffline); 

            var servers =await _serverRepository.Get(includeOffline);

            return new OkObjectResult(servers);
        }
    }
}

