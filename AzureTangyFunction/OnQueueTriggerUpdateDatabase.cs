using System;
using AzureTangyFunction.Data;
using AzureTangyFunction.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureTangyFunction
{
    public class OnQueueTriggerUpdateDatabase
    {
        private readonly AzureTangyDbContext _db;
        public OnQueueTriggerUpdateDatabase(AzureTangyDbContext db)
        {
            _db = db;
        }
        [FunctionName("OnQueueTriggerUpdateDatabase")]
        public void Run([QueueTrigger("SalesRequestInBound", Connection = "AzureWebJobsStorage")] SalesRequest myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            myQueueItem.Status = "Submitted";
            _db.SalesRequests.Add(myQueueItem);
            _db.SaveChanges();
        }
    }
}
