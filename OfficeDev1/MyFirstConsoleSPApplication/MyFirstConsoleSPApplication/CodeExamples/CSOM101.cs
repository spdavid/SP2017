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
            context.Load(w2, w => w.Title, w => w.Url);
            // Goes and gets it. 
            context.ExecuteQuery();
            Console.WriteLine(w2.Title);
            Console.WriteLine(w2.Url);

        }


    }
}
