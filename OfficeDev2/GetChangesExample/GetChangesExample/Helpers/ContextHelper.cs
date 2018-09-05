using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GetChangesExample.Helpers
{
    class ContextHelper
    {

        private static X509Certificate2 GetCert()
        {
            string thumbPrint = ConfigurationManager.AppSettings["ThumbPrint"];

            X509Certificate2 cert2 = null;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);

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

            return cert2;
        }

        public static ClientContext GetContext(string SiteUrl)
        {
            var cert = GetCert();

            string tenant = ConfigurationManager.AppSettings["tenant"];
            string applicationID = ConfigurationManager.AppSettings["ClientId"];

            OfficeDevPnP.Core.AuthenticationManager authmanager = new OfficeDevPnP.Core.AuthenticationManager();

            return authmanager.GetAzureADAppOnlyAuthenticatedContext(SiteUrl, applicationID, tenant, cert);


        }
    }
}
