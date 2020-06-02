using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace My.Function
{
    public static class HttpTriggerWithCosmosDb
    {
        [FunctionName("HttpTriggerWithCosmosDb")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "HttpTriggerWithCosmosDb/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName : "func-io-learn-db",
                collectionName : "Bookmarks",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}"
            )] BookMarkItem item,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = item.Url;

            Console.Write( item.Id);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
