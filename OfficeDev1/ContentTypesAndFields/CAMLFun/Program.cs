using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;

namespace CAMLFun
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David/"))
            {
                //XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\CAMLFun", "");
                //string templateName = "Product.xml";
                //ProvisioningTemplate template = provider.GetTemplate(templateName);
                //ctx.Web.ApplyProvisioningTemplate(template);

                //GetItemsPriceBiggerFifty(ctx);
               

             
                string templatexml = template.ToXML();
               //List list = ctx.Web.GetListByTitle("Products");
               // ctx.Load(list.DefaultView);
               // ctx.ExecuteQuery();


                Console.ReadLine();

            }
        }


        public static void GetItemsPriceBiggerFifty(ClientContext ctx)
        {

            List list = ctx.Web.Lists.GetByTitle("Products");
           
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml =
                 @"<View>  
                        <Query> 
                           <Where><And><Geq><FieldRef Name='PROD_Expiry' /><Value Type='DateTime'><Today /></Value></Geq><Geq><FieldRef Name='PROD_Year' /><Value Type='Number'>2016</Value></Geq></And></Where> 
                        </Query> 
                         <ViewFields><FieldRef Name='PROD_Price' /><FieldRef Name='Title' /><FieldRef Name='PROD_Type' /></ViewFields> 
                  </View>";


            //@"<View>  
            //       <Query> 
            //          <Where>
            //               <And>
            //                   <Geq><FieldRef Name='PROD_Price' />
            //                           <Value Type='Number'>50</Value>
            //                   </Geq>
            //                   <Leq><FieldRef Name='PROD_Price' /><Value Type='Number'>60</Value></Leq>
            //               </And>
            //           </Where>
            //           <OrderBy><FieldRef Name='PROD_Price' Ascending='FALSE' /></OrderBy> 
            //       </Query> 
            //        <ViewFields><FieldRef Name='Title' /><FieldRef Name='PROD_Type' /><FieldRef Name='PROD_Price' /></ViewFields> 
            // </View>";

            ListItemCollection items =  list.GetItems(camlQuery);

            ctx.Load(items);
            ctx.ExecuteQuery();


            foreach(ListItem item in items)
            {
                Console.WriteLine(item["Title"].ToString());
                Console.WriteLine(item["PROD_Type"].ToString());
                Console.WriteLine(item["PROD_Price"].ToString());

            }

            Console.ReadLine();

        }

        
    }
}