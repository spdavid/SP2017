using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ContextHelper
    {
        public static ClientContext GetClientContext(string url)
        {
            AuthenticationManager authmanager = new AuthenticationManager();
            string clientId = ConfigurationManager.AppSettings["clientid"];
            string clientSecret = ConfigurationManager.AppSettings["clientsecret"];
            return authmanager.GetAppOnlyAuthenticatedContext(url, clientId, clientSecret);
        }
    }

}
