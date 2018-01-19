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

            string carsId = "0x0100F1E29F5A25F14257AA14BD4BDB02DACD";


            if (rootWeb.ContentTypeExistsById(carsId))
            {
                rootWeb.DeleteContentTypeById(carsId);
            }

            rootWeb.CreateContentType("Cars", carsId, "Davids Columns");

            if (!rootWeb.FieldExistsById("{58D44996-E66B-462D-B98C-FEAAC3BFCB95}"))
            {
                FieldCreationInformation brandField = new FieldCreationInformation(FieldType.Text);
                brandField.DisplayName = "Brand";
                brandField.Id = new Guid("{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");
                brandField.InternalName = "CMS_Brand";
                brandField.Group = "Davids Fields";
                rootWeb.CreateField(brandField);
            }

            string colorFieldId = "{4A7C693C-5996-452E-A4C8-577C27469F7B}";
            if (!rootWeb.FieldExistsById(colorFieldId))
            {
                FieldCreationInformation colorField = new FieldCreationInformation(FieldType.Choice);
                colorField.DisplayName = "Color";
                colorField.Id = new Guid("colorFieldId");
                colorField.InternalName = "CMS_Color";
                colorField.Group = "Davids Fields";

                FieldChoice fc = rootWeb.CreateField<FieldChoice>(colorField);

                //Field field = rootWeb.CreateField(brandField);
                //FieldChoice fc = ctx.CastTo<FieldChoice>(field);
                fc.Choices = new string[] { "Green", "Blue", "Grey" };
                fc.Update();
                ctx.ExecuteQuery();
            }



            rootWeb.AddFieldToContentTypeById(carsId, "{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");



        }
    }
}
