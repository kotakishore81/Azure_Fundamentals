using AzureTangyFunction;
using AzureTangyFunction.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureTangyFunction
{

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureSqlDatabase");

            builder.Services.AddDbContext<AzureTangyDbContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.BuildServiceProvider();
        }
    }
}
