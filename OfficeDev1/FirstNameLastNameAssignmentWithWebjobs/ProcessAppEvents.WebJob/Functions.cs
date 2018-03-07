using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace ProcessAppEvents.WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void Install([QueueTrigger("install")] string message, TextWriter log)
        {
            string[] args = message.Split(",".ToCharArray());
            log.WriteLine("app will install on " + message);
            Common.Helpers.NameHelper.Install(args[0], args[1]);
        }
        public static void Uninstall([QueueTrigger("uninstall")] string message, TextWriter log)
        {
            log.WriteLine("app will uninstall on " + message);
            Common.Helpers.NameHelper.UnInstall(message);
        }

        public static void ChangeName([QueueTrigger("changename")] string message, TextWriter log)
        {
            string[] args = message.Split(",".ToCharArray());

            string url = args[0];
            int itemId = int.Parse(args[1]);
            Common.Helpers.NameHelper.UpdateLastName(itemId, url);
            log.WriteLine(message);
        }
    }
}
