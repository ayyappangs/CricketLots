using System.IO;
using System.Net;
using System.Threading.Tasks;
using CricketLots.Model;
using CricketLots.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CricketLots
{
    public class CreateMatchSlots
    {
        private readonly ILogger<CreateMatchSlots> _logger;

        public CreateMatchSlots(ILogger<CreateMatchSlots> log)
        {
            _logger = log;
        }

        [FunctionName("CreateMatchSlots")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MatchSlotsRequest), Description = "Parameters", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = null)] HttpRequest req)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            MatchSlotsRequest data = JsonConvert.DeserializeObject<MatchSlotsRequest>(requestBody);
            CreateMatchSlotsService createMatchSlots = new CreateMatchSlotsService();

            return new OkObjectResult(createMatchSlots.CreateMatchSlotsForIPL(data));
        }
    }
}

