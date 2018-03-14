using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Functions.Intro.Functions
{
    public static class QueueFun
    {
        [FunctionName("QueueFun")]
        public static void Run([QueueTrigger("functionqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
