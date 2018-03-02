using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace IntroToWebJobs
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();
            // how may tries before poisen queue. default is 5
            config.Queues.MaxDequeueCount = 2;
            // minimum seconds to check the queue
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(4);

            // how many items to process at once. Default is 16
            config.Queues.BatchSize = 2;

            config.UseTimers();
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
