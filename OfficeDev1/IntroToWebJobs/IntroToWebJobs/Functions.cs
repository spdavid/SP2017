using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Threading;

namespace IntroToWebJobs
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("hello")] string message, TextWriter log)
        {
            log.WriteLine(message);
           // Thread.Sleep(5000);
            // throw new Exception("something bad happened");
        }

        public static void FiveSecondTask([TimerTrigger("*/5 *  * * * *")] TimerInfo timer)
        {
            Console.WriteLine("This should run every 5 seconds");
        }

        public static void foo([QueueTrigger("hello2")] string message, TextWriter log)
        {
            log.WriteLine(message);
            // Thread.Sleep(5000);
            // throw new Exception("something bad happened");
        }




    }
}
