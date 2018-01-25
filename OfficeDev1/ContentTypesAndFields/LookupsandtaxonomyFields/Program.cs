using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LookupsandtaxonomyFields.TrickyStuff;

namespace LookupsandtaxonomyFields
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David/"))
            {
                LookupFields.CreateLookupField(ctx);
            }
        }
    }
}
