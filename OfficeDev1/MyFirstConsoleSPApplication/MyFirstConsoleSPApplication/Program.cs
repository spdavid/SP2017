﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Security;
using MyFirstConsoleSPApplication.CodeExamples;

namespace MyFirstConsoleSPApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // using statement will dispose your object when its done
            using (ClientContext ctx = Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David"))
            {
                // CodeExamples.CSOM101.GetWebTitle(ctx);
                // CodeExamples.CSOM101.UpdateTitleOfWeb(ctx, "Davids SharePoint Site");
                //  CodeExamples.CSOM101.ListAllLists(ctx);
                //CodeExamples.CSOM101.ListAllListsNotHidden(ctx);

                //CodeExamples.CSOM101.CreateDocumentLibrary(ctx);
                //CodeExamples.CSOM101.CreateGenericList(ctx);
                //CodeExamples.CSOM101.CreateTaskList(ctx);
                //CodeExamples.CSOM101.AddToLeftNav(ctx);

                //CSOM101.DiplayTitlesFromList(ctx, "Custom List");
                //CSOM101.AddItemToList(ctx, "Custom List");
            }



            Console.WriteLine("press enter to continue");
            Console.ReadLine();

        }

       
    }
}
