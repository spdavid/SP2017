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



                XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\PNPProvisioning", "");
                string templateName = "Template-LookupTest.xml";
                ProvisioningTemplate template = provider.GetTemplate(templateName);
                ctx.Web.ApplyProvisioningTemplate(template);

            }
        }

    }

}
