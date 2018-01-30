using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using System.Threading;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;

namespace PNPProvisioning
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David/"))
            {

                //Microsoft.SharePoint.Client.Field field = ctx.Web.GetFieldByInternalName("DA_Plant");

                //Console.WriteLine(field.SchemaXml);
                //Console.ReadLine();



                //XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\PNPProvisioning", "");
                //string templateName = "Template-LookupAsignment.xml";
                //ProvisioningTemplate template = provider.GetTemplate(templateName);
                //ctx.Web.ApplyProvisioningTemplate(template);

                //AddItemsToEmployeeList(ctx);

                ShowAllItemsInConsole(ctx);
            }
           
        }

        private static void ShowAllItemsInConsole(ClientContext ctx)
        {
            List list = ctx.Web.Lists.GetByTitle("Employees");

            ListItemCollection items = list.GetItems(CamlQuery.CreateAllItemsQuery());
            ctx.Load(items);
            ctx.ExecuteQuery();


            foreach (ListItem item in items)
            {


                Console.WriteLine(item["Title"].ToString());
                if (item["DA_Employee"] != null)
                {
                    FieldUserValue employee = item["DA_Employee"] as FieldUserValue;
                    Console.WriteLine(employee.LookupValue);
                    Console.WriteLine(employee.Email);
                }
                if (item["DA_EmpPic"] != null)
                {
                    FieldUrlValue picField = item["DA_EmpPic"] as FieldUrlValue;
                    Console.WriteLine(picField.Description);
                    Console.WriteLine(picField.Url);
                }
                if (item["DA_Linkedin"] != null)
                {
                    FieldUrlValue linkedinValue = item["DA_Linkedin"] as FieldUrlValue;
                    Console.WriteLine(linkedinValue.Description);
                    Console.WriteLine(linkedinValue.Url);
                }
                //   int.Parse(item["DAV_empage"].ToString());
                if (item["DAV_empage"] != null)
                    Console.WriteLine(item["DAV_empage"].ToString());
                if (item["DA_Education"] != null)
                    Console.WriteLine(item["DA_Education"].ToString());

                if (item["DA_EmployeeType"] != null)
                {
                    FieldLookupValue empType = item["DA_EmployeeType"] as FieldLookupValue;
                    Console.WriteLine(empType.LookupValue);
                }
                if (item["DA_Manager"] != null)
                {
                    FieldLookupValue manager = item["DA_Manager"] as FieldLookupValue;
                    Console.WriteLine(manager.LookupValue);
                }

                Console.WriteLine();
                Console.WriteLine("###############################################");
                Console.WriteLine();



            }


            Console.WriteLine("press key to continue");
            Console.ReadLine();

        }

        public static void AddItemsToEmployeeList(ClientContext ctx)
        {

            // get info we need for the add item

            // get the user by their login name so that we can get the id
            // Ensure user makes sure that the user exists in the hidden
            // user list in the site collection
            Microsoft.SharePoint.Client.User user = ctx.Web.EnsureUser("Luis@folkis2017.onmicrosoft.com");
            ctx.Load(user, u =>  u.Id);
            ctx.ExecuteQuery();

           
            // add a new item
            ListItem item = ctx.Web.Lists.GetByTitle("Employees").AddItem(new ListItemCreationInformation());



            item["Title"] = "Luis";
            item["DA_Employee"] = user.Id;
            item["DA_EmpPic"] = new FieldUrlValue() { Description = "Luis Picture", Url = "http://www.bravotv.com/sites/nbcubravotv/files/field_blog_image/2016/07/dish-070716-luis-quitting-real-estate.jpg" };
            item["DA_Linkedin"] = new FieldUrlValue() { Description = "Luis Linkein", Url = "https://linkein.com/someone" };
            item["DAV_empage"] = 25;
            item["DA_Education"] = "Doctors Degree";
            item["DA_EmployeeType"] = 1;
            item["DA_Manager"] = 2;

            item.Update();
            ctx.ExecuteQuery();






        }

    }

}
