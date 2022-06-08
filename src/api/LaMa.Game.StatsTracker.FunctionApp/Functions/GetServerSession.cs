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
    public class GetServerSession
    {
        private readonly ILogger<GetServerSession> _logger;
        private readonly IServerRepository _serverRepository;

        public GetServerSession(ILogger<GetServerSession> log, IServerRepository serverRepository)
        {
            _logger = log;
            _serverRepository = serverRepository;
        }

        [FunctionName("GetServerSession")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "servers/{ip}")] HttpRequest req, string ip)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var servers = _serverRepository.GetServerSession(ip);

            return new OkObjectResult(servers);
        }
    }
}

