using System;
using System.Collections.Generic;
using System.Linq;
using AzureTangyFunction.Data;
using AzureTangyFunction.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureTangyFunction
{
    public class UpdateStatusToCompletedAndSendEmail
    {
        private readonly AzureTangyDbContext _db;
        public UpdateStatusToCompletedAndSendEmail(AzureTangyDbContext db)
        {
            _db = db;
        }

        [FunctionName("UpdateStatusToCompletedAndSendEmail")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            IEnumerable<SalesRequest> salesRequestFromDb = _db.SalesRequests.Where(u => u.Status == "Image Processed");
            foreach (var salesReq in salesRequestFromDb)
            {
                //for each request update status
                salesReq.Status = "Completed";
            }

            _db.UpdateRange(salesRequestFromDb);
            _db.SaveChanges();
        }
    }
}
