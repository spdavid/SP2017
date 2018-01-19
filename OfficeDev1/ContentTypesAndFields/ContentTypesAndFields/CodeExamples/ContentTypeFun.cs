using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTypesAndFields.CodeExamples
{
    public class ContentTypeFun
    {

        public static void MyFirstContentType(ClientContext ctx)
        {

            Web rootWeb = ctx.Site.RootWeb;
            CleanUP(rootWeb);

            CreateContentTypes(rootWeb);
            CreateFields(ctx, rootWeb);

            rootWeb.AddFieldToContentTypeById(Constants.CAR_CT_ID, "{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");
            rootWeb.AddFieldToContentTypeById(Constants.CAR_CT_ID, Constants.COLOR_FIELD_ID);
            rootWeb.AddFieldToContentTypeById(Constants.CAR_CT_ID, Constants.YEAR_FIELD_ID);
            SetupList(ctx, rootWeb);

        }

        private static void SetupList(ClientContext ctx, Web rootWeb)
        {
            List list = rootWeb.CreateList(ListTemplateType.GenericList, "Cars", false, enableContentTypes: true);

            list.AddContentTypeToListById(Constants.CAR_CT_ID, true);


            ctx.Load(list.ContentTypes);
            ctx.ExecuteQuery();

            for (int i = list.ContentTypes.Count() - 1; i >= 0; i--)
            {
                ContentType ct = list.ContentTypes[i];
                if (!ct.Id.StringValue.StartsWith(Constants.CAR_CT_ID))
                {
                    list.RemoveContentTypeByName(ct.Name);
                }
            }

            list.DefaultView.ViewFields.Add("CMS_Brand");
            list.DefaultView.ViewFields.Add("CMS_Year");
            list.DefaultView.ViewFields.Add("CMS_Color");
            list.DefaultView.Update();
            ctx.ExecuteQuery();
        }

        private static void CreateFields(ClientContext ctx, Web rootWeb)
        {


            // Brand Field
            // note that we have the field id not in a variable or constant. This is
            // to demonstrate how messy it can be. 
            if (!rootWeb.FieldExistsById("{58D44996-E66B-462D-B98C-FEAAC3BFCB95}"))
            {
                FieldCreationInformation brandField = new FieldCreationInformation(FieldType.Text);
                brandField.DisplayName = "Brand";
                brandField.Id = new Guid("{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");
                brandField.InternalName = "CMS_Brand";
                brandField.Group = "Davids Fields";
                rootWeb.CreateField(brandField);
            }


            // create color field

            //if (rootWeb.FieldExistsById(colorFieldId))
            //{
            //    Field field =  rootWeb.GetFieldById(new Guid(colorFieldId));
            //    field.DeleteObject();
            //    ctx.ExecuteQuery();
            //}

            if (!rootWeb.FieldExistsById(Constants.COLOR_FIELD_ID))
            {
                FieldCreationInformation colorField = new FieldCreationInformation(FieldType.Choice);
                colorField.DisplayName = "Color";
                colorField.Id = new Guid(Constants.COLOR_FIELD_ID);
                colorField.InternalName = "CMS_Color";
                colorField.Group = "Davids Fields";
                FieldChoice fc = rootWeb.CreateField<FieldChoice>(colorField);
                //Field field = rootWeb.CreateField(brandField);
                //FieldChoice fc = ctx.CastTo<FieldChoice>(field);
                fc.Choices = new string[] { "Green", "Blue", "Grey" };
                fc.Update();
                ctx.ExecuteQuery();
            }



            if (!rootWeb.FieldExistsById(new Guid(Constants.YEAR_FIELD_ID)))
            {
                FieldCreationInformation carYearField = new FieldCreationInformation(FieldType.Number);
                carYearField.DisplayName = "Year";
                carYearField.Id = new Guid(Constants.YEAR_FIELD_ID);
                carYearField.InternalName = "CMS_Year";
                carYearField.Group = "Davids Fields";
                rootWeb.CreateField(carYearField);
            }
        }


        private static void CleanUP(Web rootWeb)
        {
            if (rootWeb.ContentTypeExistsById(Constants.CAR_CT_ID))
            {
                rootWeb.GetListByTitle("Cars").DeleteObject();
                ClientContext ctx = rootWeb.Context as ClientContext;
                ctx.ExecuteQuery();
                rootWeb.DeleteContentTypeById(Constants.CAR_CT_ID);
            }
        }

        private static void CreateContentTypes(Web rootWeb)
        {
            // create content type

            rootWeb.CreateContentType("Cars", Constants.CAR_CT_ID, "Davids Columns");
         
        }
    }
}
