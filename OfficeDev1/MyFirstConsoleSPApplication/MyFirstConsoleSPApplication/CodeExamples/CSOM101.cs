using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleSPApplication.CodeExamples
{
    public class CSOM101
    {
        public static void GetWebTitle(ClientContext context)
        {
            //Web w1 = context.Web;

            //// take your order
            //context.Load(w1);
            //// Goes and gets it. Takes about 400 ms or longer
            //context.ExecuteQuery();
            //Console.WriteLine(w1.Title);
            //Console.WriteLine(w1.Url);

            // the below code is faster as we are only requesting 
            // the properties we want from the web object
            Web w2 = context.Web;
            // only get the propeties you need
            context.Load(w2,
                w => w.Title,
                w => w.Url);
            // Goes and gets it. 
            context.ExecuteQuery();
            Console.WriteLine(w2.Title);
            Console.WriteLine(w2.Url);

        }

        public static void UpdateTitleOfWeb(ClientContext ctx, string NewTitle)
        {
            Web web = ctx.Web;

            web.Title = NewTitle;
            web.Update();

            ctx.ExecuteQuery();

        }

        public static void ListAllLists(ClientContext ctx)
        {
            ListCollection lists = ctx.Web.Lists;
            // ctx.Load(lists); Takes longer as it gets everything

            ctx.Load(lists, lsts => lsts.Include(
                l => l.Title,
                l => l.DefaultViewUrl,
                l => l.Hidden));
            ctx.ExecuteQuery();


            foreach (var list in lists)
            {
                //if (!list.Hidden)
                //{
                Console.WriteLine(list.Title);
                Console.WriteLine(list.DefaultViewUrl);
                //}
            }

        }

        public static void ListAllListsNotHidden(ClientContext ctx)
        {
            ListCollection lists = ctx.Web.Lists;
            // ctx.Load(lists); Takes longer as it gets everything
            var results = ctx.LoadQuery(lists.Where(list => list.Hidden == false));
            ctx.ExecuteQuery();
            foreach (var list in results)
            {
                ctx.Load(list, l => l.DefaultViewUrl);
            }

            ctx.ExecuteQuery();



            foreach (var list in results)
            {
                //if (!list.Hidden)
                //{
                Console.WriteLine(list.Title);
                Console.WriteLine(list.DefaultViewUrl);
                //}
            }

        }

        public static void CreateDocumentLibrary(ClientContext ctx)
        {
            ListCreationInformation info = new ListCreationInformation();
            info.Title = "New Doc Library";
            info.TemplateType = 101;
            info.Description = "davids cool library";
            info.Url = "NewDocLib";
            ctx.Web.Lists.Add(info);

            ctx.ExecuteQuery();
        }

        public static void CreateGenericList(ClientContext ctx)
        {
            var lists = ctx.Web.Lists;
            var results = ctx.LoadQuery(lists.Where(list => list.Title == "Custom List"));
            ctx.Web.Context.ExecuteQuery();

            // same as below
            // if (results.Count() > 0)
            if (!results.Any())
            {
                ListCreationInformation info = new ListCreationInformation();
                info.Title = "Custom List";
                info.TemplateType = 100;
                info.Description = "Custom List";
                info.Url = "lists/customlist";
                ctx.Web.Lists.Add(info);

                ctx.ExecuteQuery();
            }
            else
            {
                Console.WriteLine("List already exists. Give it another url and name");
            }
        }

        public static void CreateTaskList(ClientContext ctx)
        {
            ListCreationInformation info = new ListCreationInformation();
            info.Title = "My Tasks";
            info.Url = "lists/mytasks";
            info.TemplateType = (int)ListTemplateType.Tasks;
            // or  info.TemplateType = 107;
            ctx.Web.Lists.Add(info);

            ctx.ExecuteQuery();
        }

        public static void AddToLeftNav(ClientContext ctx)
        {
            NavigationNodeCreationInformation nodeInfo = new NavigationNodeCreationInformation();
            nodeInfo.Url = "https://www.google.com";
            nodeInfo.Title = "Google";
            nodeInfo.AsLastNode = true;
            ctx.Web.Navigation.QuickLaunch.Add(nodeInfo);

            ctx.ExecuteQuery();
        }


        public static void DiplayTitlesFromList(ClientContext ctx, string listName)
        {
          ListItemCollection items = ctx.Web.Lists.GetByTitle(listName).GetItems(CamlQuery.CreateAllItemsQuery());
            ctx.Load(items);
            ctx.ExecuteQuery();

            foreach (var item in items)
            {
                Console.WriteLine(item["Title"].ToString());
            }
        }

        public static void AddItemToList(ClientContext ctx, string listName)
        {

           ListItem item = ctx.Web.Lists.GetByTitle(listName).AddItem(new ListItemCreationInformation());
            item["Title"] = "Hello World";
            item.Update();
            ctx.ExecuteQuery();

        }

    }
}
