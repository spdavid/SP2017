﻿using Microsoft.SharePoint.Client;
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

            FieldCreationInformation brandField = new FieldCreationInformation(FieldType.Text);
            brandField.DisplayName = "Brand";
            brandField.Id = new Guid("{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");
            brandField.InternalName = "CMS_Brand";
            brandField.Group = "Davids Fields";
            rootWeb.CreateField(brandField);

            rootWeb.AddFieldToContentTypeById(carsId, "{58D44996-E66B-462D-B98C-FEAAC3BFCB95}");



        }
    }
}