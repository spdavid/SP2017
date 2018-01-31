using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxonomyFun
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = Common.Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David/"))
            {
                // CreateTerms(ctx);
                CreateTaxonomyField(ctx);
            }
        }

        static void CreateTaxonomyField(ClientContext ctx)
        {
          //  ctx.Web.GetFieldById("{038176D8-90A9-4FE6-869D-0FD4CFA4D7E7}".ToGuid()).DeleteObject();

            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            // guid is from the termset i created below.
            TermSet termset = store.GetTermSet("{549859DF-A871-4248-A8F2-49A3BF77951E}".ToGuid());

            ctx.Load(termset);
            ctx.ExecuteQuery();

            TaxonomyFieldCreationInformation info = new TaxonomyFieldCreationInformation();
            info.DisplayName = "Animal";
            // field term id. New Guid
            info.Id = "{038176D8-90A9-4FE6-869D-0FD4CFA4D7E7}".ToGuid();
            info.InternalName = "DA_TAXAnimal";
            // connect it to the termset we created below
            info.TaxonomyItem = termset;
            info.Group = "Davids Fields";
            ctx.Web.CreateTaxonomyField(info);

        }

        static void CreateTerms(ClientContext ctx)
        {
            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();

            TermGroup group = store.GetTermGroupByName("David");
            if (group == null)
            {
                group = store.CreateTermGroup("David", "{B49D90C1-3E56-442C-914B-52364893DA88}".ToGuid());
            }

            TermSet termset = group.EnsureTermSet("Animals", "{549859DF-A871-4248-A8F2-49A3BF77951E}".ToGuid(), 1033);


            termset.CreateTerm("Dog", 1033, "{56E0153A-7CA0-42E2-9E24-29E998EC47AB}".ToGuid());
            Term cat = termset.CreateTerm("Cat", 1033, "{237D6C0E-D4B5-4C48-A297-11770738903A}".ToGuid());
            termset.CreateTerm("Horse", 1033, "{F078BA36-752A-408D-8C70-87A639E06D5B}".ToGuid());

            ctx.Load(cat);
            ctx.ExecuteQuery();


            cat.CreateLabel("Katt", 1053, false);
            cat.CreateLabel("Feline", 1033, false);

            ctx.ExecuteQuery();


        }
    }
}
