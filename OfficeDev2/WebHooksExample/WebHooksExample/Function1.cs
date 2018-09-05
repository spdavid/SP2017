using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using WebHooksExample.Models;

namespace WebHooksExample
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("sharepointlistwebhookeventazuread", Connection = "AzureWebJobsStorage")]string notification, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {notification}");



            NotificationModel notif =  JsonConvert.DeserializeObject<NotificationModel>(notification);



            using (ClientContext ctx = Helpers.ContextHelper.GetContext("https://zalolabs.sharepoint.com/sites/DEMO/"))
            {
                List list = ctx.Web.GetListById(notif.Resource.ToGuid());

                log.Info("checking changes in list: " + list.Title);
                ChangeQuery query = new ChangeQuery(false, false)
                {
                    Item = true,
                    Add = true,
                    Update = true,
                    DeleteObject = true,
                    ChangeTokenStart = new ChangeToken
                    {
                        StringValue = string.Format("1;3;{0};{1};-1", list.Id.ToString(), DateTime.Now.AddMinutes(-50).ToUniversalTime().Ticks.ToString())
                    },
                    ChangeTokenEnd = list.CurrentChangeToken
                };

                ChangeCollection changes = list.GetChanges(query);

                ctx.Load(changes);
                ctx.ExecuteQuery();

                foreach (ChangeItem change in changes)
                {

                    log.Info(change.ChangeType.ToString());
                    log.Info(change.ItemId.ToString());
                    AddItemIdToQueue(change.ItemId.ToString());

                }




            }
           


        }

        public static void  AddItemIdToQueue(string itemId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=davidstrorage;AccountKey=3pj3JlJ5F3uG2+ms2XVvwmhG8DDhWiwJucAzNjmM8ijt5KtmeRcxISqXpZRzACHot2kHQ3lxX5kvyeTluZtV/g==;BlobEndpoint=https://davidstrorage.blob.core.windows.net/;QueueEndpoint=https://davidstrorage.queue.core.windows.net/;TableEndpoint=https://davidstrorage.table.core.windows.net/;FileEndpoint=https://davidstrorage.file.core.windows.net/;");
            // Get queue... create if does not exist.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("itemneedsprocessing");
            queue.CreateIfNotExists();
           
            queue.AddMessage(new CloudQueueMessage(itemId));
        }

    }
}
