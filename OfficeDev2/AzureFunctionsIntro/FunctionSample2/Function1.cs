using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;

namespace FunctionSample2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string thumbprint = "F65918301ACDCAA86B061D123B9F36C23E667864";
            string siteUrl = "https://folkis2017.sharepoint.com/sites/david";
            string tenant = "folkis2017.onmicrosoft.com";
            string applicationID = "b73a2784-17dd-4040-ab28-b00504ab0526";

            X509Certificate2 cert2 = null;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

                if (col == null || col.Count == 0)
                {

                    return null;
                }
                cert2 = col[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                store.Close();
            }

            OfficeDevPnP.Core.AuthenticationManager authmanager = new OfficeDevPnP.Core.AuthenticationManager();



            using (ClientContext ctx = authmanager.GetAzureADAppOnlyAuthenticatedContext(siteUrl, applicationID, tenant, cert2))
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                log.Info("Your Site Name is: " + ctx.Web.Title);
                return req.CreateResponse(HttpStatusCode.OK, $"Your Site Name is: " + ctx.Web.Title);
            }


        }
    }
}
