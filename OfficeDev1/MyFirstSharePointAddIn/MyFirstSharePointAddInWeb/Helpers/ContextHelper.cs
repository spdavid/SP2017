using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstSharePointAddInWeb.Helpers
{
    public class ContextHelper
    {

        public static ClientContext GetContext()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
            return spContext.CreateUserClientContextForSPHost();
        }
       
           
    }
}