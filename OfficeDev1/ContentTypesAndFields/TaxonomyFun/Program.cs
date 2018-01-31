using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Entities;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
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
                // CreateTaxonomyField(ctx);


                //XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\TaxonomyFun", "");
                //string templateName = "Template.xml";
                //ProvisioningTemplate template = provider.GetTemplate(templateName);
                //ctx.Web.ApplyProvisioningTemplate(template);


                //UpdatingTaxonomyField(ctx);

                //var view = ctx.Web.GetListByTitle("Super Heros").DefaultView;
                //  ctx.Load(view);
                //  ctx.ExecuteQuery();
                // ctx.Web.GetListByTitle("Super Heros").DeleteObject();

                //ctx.ExecuteQuery();



                // super heros assignment
                //XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\TaxonomyFun", "");
                //string templateName = "TemplateSuperHeros.xml";
                //ProvisioningTemplate template = provider.GetTemplate(templateName);
                //ctx.Web.ApplyProvisioningTemplate(template);

                //  AddToSUperHeroList(ctx);
                ReadToSUperHeroList(ctx);
                Console.ReadLine();
            }
        }

        private static void ReadToSUperHeroList(ClientContext ctx)
        {
            var list = ctx.Web.GetListByTitle("Super Heros");

            ListItemCollection items = list.GetItems(CamlQuery.CreateAllItemsQuery());

            ctx.Load(items);
            ctx.ExecuteQuery();

            foreach(ListItem item in items)
            {
                if (item["Title"] != null)
                    Console.WriteLine(item["Title"].ToString());

                var superPows =  item["DA_SuperPow"] as TaxonomyFieldValue;
                Console.WriteLine(superPows.Label);

                var weapons = item["DA_Weapon"] as TaxonomyFieldValueCollection;
                foreach (var weapon in weapons)
                {
                    Console.WriteLine(weapon.Label);
                }
            }
        }

        static void AddToSUperHeroList(ClientContext ctx)
        {
            var list = ctx.Web.GetListByTitle("Super Heros");

            var store =  ctx.Site.GetDefaultSiteCollectionTermStore();
            var superPowerTerm = store.GetTerm("{95194EEC-E537-4BB0-AC3C-82BA4B56B56A}".ToGuid());
            var weapon1term = store.GetTerm("{F7735448-B8AD-4528-B989-CD76AC23C835}".ToGuid());
            var weapon2term = store.GetTerm("{B2841C21-CA5A-4F9D-9B94-FD242AF7A9C2}".ToGuid());
            ctx.Load(superPowerTerm);
            ctx.Load(weapon1term);
            ctx.Load(weapon2term);
            ctx.ExecuteQuery();

            List<KeyValuePair<Guid, String>> weapons = new List<KeyValuePair<Guid, string>>();
            weapons.Add(new KeyValuePair<Guid, string>(weapon1term.Id, weapon1term.Name));
            weapons.Add(new KeyValuePair<Guid, string>(weapon2term.Id, weapon2term.Name));

            ListItem item = list.AddItem(new ListItemCreationInformation());
            item["Title"] = "Wolverine";
            item.Update();

            item.SetTaxonomyFieldValue("{E6D17C98-DFC0-4A20-B644-A4AEDEC1C2C5}".ToGuid(), superPowerTerm.Name, superPowerTerm.Id);
            item.SetTaxonomyFieldValues("{E9DBD5B5-3DC2-4057-91A8-BAC03C02EF8F}".ToGuid(), weapons);

            ctx.ExecuteQuery();

        }

        static void ReadingFromTaxonomyField(ClientContext ctx)
        {
            // i have added one document manually after the xml import. 
           List list = ctx.Web.GetListByTitle("Important documents");
           ListItem item = list.GetItemById(2);
            ctx.Load(item);

            ctx.ExecuteQuery();

            TaxonomyFieldValue taxValue = item["DA_DOCTYPE"] as TaxonomyFieldValue;
            Console.WriteLine(taxValue.Label);
            Console.ReadLine();
        }


        static void UpdatingTaxonomyField(ClientContext ctx)
        {
            // i have added one document manually after the xml import. 
            List list = ctx.Web.GetListByTitle("Important documents");
            ListItem item = list.GetItemById(2);
            ctx.Load(item);
            ctx.ExecuteQuery();

            
            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            var term = store.GetTerm("{36575A1B-B081-4964-A50B-53841F741D5F}".ToGuid());
            ctx.Load(term);
            ctx.ExecuteQuery();
            // non pnp way.
            //TaxonomyField field = list.GetFieldById<TaxonomyField>("{A76D725F-8324-4152-AB83-4920E4342368}".ToGuid());
            // field.SetFieldValueByTerm(item, term, 1033);
         

            // pnp way
            item.SetTaxonomyFieldValue("{A76D725F-8324-4152-AB83-4920E4342368}".ToGuid(), term.Name, term.Id);



            item.Update();
             ctx.ExecuteQuery();


            Console.ReadLine();
        }


        static void CreateTaxonomyField(ClientContext ctx)
        {
          //  ctx.Web.GetFieldById("{038176D8-90A9-4FE6-869D-0FD4CFA4D7E7}".ToGuid()).DeleteObject();

            TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
            // guid is from the termset i created below.
            Microsoft.SharePoint.Client.Taxonomy.TermSet termset = store.GetTermSet("{549859DF-A871-4248-A8F2-49A3BF77951E}".ToGuid());

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

            Microsoft.SharePoint.Client.Taxonomy.TermGroup group = store.GetTermGroupByName("David");
            if (group == null)
            {
                group = store.CreateTermGroup("David", "{B49D90C1-3E56-442C-914B-52364893DA88}".ToGuid());
            }

            Microsoft.SharePoint.Client.Taxonomy.TermSet termset = group.EnsureTermSet("Animals", "{549859DF-A871-4248-A8F2-49A3BF77951E}".ToGuid(), 1033);


            termset.CreateTerm("Dog", 1033, "{56E0153A-7CA0-42E2-9E24-29E998EC47AB}".ToGuid());
            Microsoft.SharePoint.Client.Taxonomy.Term cat = termset.CreateTerm("Cat", 1033, "{237D6C0E-D4B5-4C48-A297-11770738903A}".ToGuid());
            termset.CreateTerm("Horse", 1033, "{F078BA36-752A-408D-8C70-87A639E06D5B}".ToGuid());

            ctx.Load(cat);
            ctx.ExecuteQuery();


            cat.CreateLabel("Katt", 1053, false);
            cat.CreateLabel("Feline", 1033, false);

            ctx.ExecuteQuery();


        }
    }
}
