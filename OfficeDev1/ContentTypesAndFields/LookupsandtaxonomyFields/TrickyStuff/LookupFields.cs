using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupsandtaxonomyFields.TrickyStuff
{
    public class LookupFields
    {
        public static void  CreateLookupField(ClientContext ctx)
        {

            // clean up stuff

            //ctx.Web.GetListByTitle("Plants").DeleteObject();
            //ctx.Web.GetListByTitle("PlantTypes").DeleteObject();
            //ctx.ExecuteQuery();


            //Field fieldtoDelete = ctx.Web.GetFieldById("{450827CE-588F-487C-B0B3-7907D57049F1}".ToGuid());
            //fieldtoDelete.DeleteObject();
            //ctx.ExecuteQuery();


            if (!ctx.Web.ListExists("PlantTypes"))
            {
                List list = ctx.Web.CreateList(ListTemplateType.GenericList, "PlantTypes", false);

                ListItem item1 = list.AddItem(new ListItemCreationInformation());
                item1["Title"] = "Tree";
                item1.Update();

                ListItem item2 = list.AddItem(new ListItemCreationInformation());
                item2["Title"] = "Shrub";
                item2.Update();

                ListItem item3 = list.AddItem(new ListItemCreationInformation());
                item3["Title"] = "Leafy";
                item3.Update();

                ListItem item4 = list.AddItem(new ListItemCreationInformation());
                item4["Title"] = "Flower";
                item4.Update();

                ctx.ExecuteQuery();

            }


            string plantId = "{450827CE-588F-487C-B0B3-7907D57049F1}";

            if (!ctx.Web.FieldExistsById(plantId))
            {

                List LookupList = ctx.Web.GetListByTitle("PlantTypes");

                FieldCreationInformation lookupField = new FieldCreationInformation(FieldType.Lookup);

                lookupField.Id = plantId.ToGuid();
                lookupField.InternalName = "DA_Plant";
                lookupField.Group = "Davids columns";
                lookupField.DisplayName = "Plant Types";

                FieldLookup field = ctx.Web.CreateField<FieldLookup>(lookupField);
                field.LookupList = LookupList.Id.ToString() ;
                field.LookupField = "Title";
                field.Update();

                ctx.ExecuteQuery();

            }

            if (!ctx.Web.ListExists("Plants"))
            {
                List list = ctx.Web.CreateList(ListTemplateType.GenericList, "Plants", false);

                Field field = ctx.Web.GetFieldById(plantId.ToGuid());
                list.Fields.Add(field);
                list.Update();

                ctx.ExecuteQuery();

            }


            //ctx.Web.GetListByTitle("Plants");

            ListItem item22 = ctx.Web.Lists.GetByTitle("Plants").AddItem(new ListItemCreationInformation());

            item22["Title"] = "Tulip";
            item22["DA_Plant"] = 4;
            item22.Update();

            ctx.ExecuteQuery();

            ListItemCollection items = ctx.Web.Lists.GetByTitle("Plants").GetItems(CamlQuery.CreateAllItemsQuery());
            ctx.Load(items);
            ctx.ExecuteQuery();


            foreach (ListItem item in items)
            {
                Console.WriteLine(item["Title"]);

                FieldLookupValue plantType = item["DA_Plant"] as FieldLookupValue;
                Console.WriteLine(plantType.LookupId);
                Console.WriteLine(plantType.LookupValue);
                
            }

            Console.ReadLine();




        }

        }
}
