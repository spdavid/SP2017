using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Common.Models;
using Newtonsoft.Json;
using Common.Helpers;

namespace WebHookExample.Functions
{
    public static class Notifications
    {
        [FunctionName("Notifications")]
        public static void Run([QueueTrigger("somethinghappened", Connection = "AzureWebJobsStorage")]string message, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {message}");
          
            NotificationModel notification = JsonConvert.DeserializeObject<NotificationModel>(message);

           string logs = NotificationHelper.ProcessNotification(notification);

            log.Info(logs);
        }
    }
}
