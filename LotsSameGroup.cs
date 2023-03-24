using System.Collections.Generic;
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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CricketLots
{
    public class LotsSameGroup
    {
        [FunctionName("GetLotsSameGroup")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(LotsRequest), Description = "Parameters", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(List<string>), Description = "The OK response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            LotsRequest data = JsonConvert.DeserializeObject<LotsRequest>(requestBody);
           
            CreateLots createLots = new CreateLots();           
           
            return new OkObjectResult(createLots.GetLotsSameGroup(data));
        }
    }
}

