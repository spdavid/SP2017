using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class QueueHelper
    {
        public static object CloudConfigurationManager { get; private set; }

        public static void AddToQueue(string queuename, string message)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["storageaccount"]);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue messageQueue = queueClient.GetQueueReference(queuename);
            messageQueue.CreateIfNotExists();
            CloudQueueMessage msg = new CloudQueueMessage(message);
            messageQueue.AddMessage(msg);
            
        }

    }
}
