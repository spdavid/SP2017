using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Functions.Intro.Functions
{
    public static class TimerFun
    {
        [FunctionName("TimerFun")]
        public static void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {

            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
