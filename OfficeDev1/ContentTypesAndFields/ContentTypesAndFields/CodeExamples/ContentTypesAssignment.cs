using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTypesAndFields.CodeExamples
{
   public  class ContentTypesAssignment
    {
        public static void CreateBookCT(ClientContext ctx)
        {

            //Create a content type called "Book" that inherits from Item
            //Create Add the following fields
            //	1. Book Type - Choice
            //	2. Author - Text
            //	3. Date Release - DateTime
            //Description - MultiText With Rich Text Formatting : TIP (Note Field)

            string bookCT = "0x0100E1B5ADE3E3524356BBC369B4238959BB";

            Web web = ctx.Site.RootWeb;

            //web.GetListByTitle("Books").DeleteObject();
            //ctx.ExecuteQuery();

            //web.DeleteContentTypeById(bookCT);
           if(! web.ContentTypeExistsById(bookCT))
            {
                web.CreateContentType("Book", bookCT, "Davids ContentTypes");
            }

            string bookTypeFieldId = "{EB31EFF6-4103-4773-957F-2539C7644E5C}";

            if (!web.FieldExistsById(new Guid(bookTypeFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Choice);
                info.Id = bookTypeFieldId.ToGuid();
                info.InternalName = "DAV_BookType";
                info.DisplayName = "Book Type";
                info.Group = "Davids Columns";
                FieldChoice field = web.CreateField<FieldChoice>(info);
                field.Choices = new string[] { "Romance", "Fantasy", "Fiction", "Biology" };
                field.Update();
                ctx.ExecuteQuery();
            }


            string AuthorFieldId = "{485A1E7A-051C-42A4-B845-68BE284D0701}";

            if (!web.FieldExistsById(new Guid(AuthorFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Text);
                info.Id = AuthorFieldId.ToGuid();
                info.InternalName = "DAV_Author";
                info.DisplayName = "Author";
                info.Group = "Davids Columns";
                web.CreateField(info);
            }

            string ReleaseDateId = "{A45AE20C-B9C3-4D31-9409-7554F9D57A4C}";
            //web.RemoveFieldById(ReleaseDateId);
            if (!web.FieldExistsById(new Guid(ReleaseDateId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.DateTime);
                info.Id = ReleaseDateId.ToGuid();
                info.InternalName = "DAV_ReleaseDate";
                info.DisplayName = "Release Date";
                info.Group = "Davids Columns";
                FieldDateTime field = web.CreateField<FieldDateTime>(info);
                field.DisplayFormat = DateTimeFieldFormatType.DateOnly;
                field.Update();
                ctx.ExecuteQuery();

            }

            string DescriptoinId = "{8B45056F-EE53-4499-B442-7FECA82A14A7}";

            //web.RemoveFieldById(DescriptoinId);
            if (!web.FieldExistsById(new Guid(DescriptoinId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Note);
                info.Id = DescriptoinId.ToGuid();
                info.InternalName = "DAV_Description";
                info.DisplayName = "Description";
                info.Required = true;
                info.Group = "Davids Columns";
               FieldMultiLineText descField = web.CreateField<FieldMultiLineText>(info);
                descField.RichText = true;
                descField.AllowHyperlink = true;
                descField.NumberOfLines = 10;
                descField.Update();
                ctx.ExecuteQuery();
            }

            web.AddFieldToContentTypeById(bookCT, bookTypeFieldId);
            web.AddFieldToContentTypeById(bookCT, AuthorFieldId);
            web.AddFieldToContentTypeById(bookCT, ReleaseDateId);
            web.AddFieldToContentTypeById(bookCT, DescriptoinId, true);


            if (!web.ListExists("Books"))
            {
                List list = web.CreateList(ListTemplateType.GenericList, "Books", false, urlPath: "lists/books", enableContentTypes: true);
                list.AddContentTypeToListById(bookCT, true);

                View listView = list.DefaultView;
                listView.ViewFields.Add("DAV_BookType");
                listView.ViewFields.Add("DAV_Author");
                listView.ViewFields.Add("DAV_ReleaseDate");
                listView.ViewFields.Add("DAV_Description");
                listView.Update();
                ctx.ExecuteQueryRetry();
            }


            List booksList = web.GetListByTitle("Books");

            ListItem item = booksList.AddItem(new ListItemCreationInformation());

            item["Title"] = "Mistborn";
            item["DAV_BookType"] = "Fantasy";
            item["DAV_Author"] = "Brandon Sanderson";
            item["DAV_ReleaseDate"] = DateTime.Parse("2001-02-12");
            item["DAV_Description"] = "this is the descriptiong \n\n is this a new line?";

            item.Update();
            ctx.ExecuteQueryRetry();


           //ListItemCollection items = booksList.GetItems(CamlQuery.CreateAllItemsQuery());
           // ctx.Load(items);
           // ctx.ExecuteQuery();


          




        }
    }
}
