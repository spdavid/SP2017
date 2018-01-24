using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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



        public static void CreateCV(ClientContext ctx)
        {


            //Create a Content Type Called CV that inherits from Document
            //	1. Title - Exists already
            //	2. Picture - Picture Column 
            //	3. User - Person Field
            //	4.  Is Active - Yes / No


            // doc content type = 0x0101
            // seperator 00
            // new guid {0F34D58A-C07F-4660-82CF-A31B4AF3B231}
            string CVCT = "0x0101000F34D58AC07F466082CFA31B4AF3B231";

            Web web = ctx.Site.RootWeb;

           
            if (!web.ContentTypeExistsById(CVCT))
            {
                web.CreateContentType("CV", CVCT, "a Davids ContentTypes");
            }



            string pictureFieldId = "{5FD1BD7D-C223-4584-9F8E-2F372F9D7B79}";

            if (!web.FieldExistsById(new Guid(pictureFieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.URL);
                info.Id = pictureFieldId.ToGuid();
                info.InternalName = "DAV_Pic";
                info.DisplayName = "Picture";
                info.Group = "a Davids Columns";
                FieldUrl picField = web.CreateField<FieldUrl>(info);
                picField.DisplayFormat = UrlFieldFormatType.Image;
                picField.Update();
                ctx.ExecuteQuery();

            }

            string userieldId = "{25C045F9-F97A-4B7B-AAB5-876A216DCFC9}";

            if (!web.FieldExistsById(new Guid(userieldId)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.User);
                info.Id = userieldId.ToGuid();
                info.InternalName = "DAV_User";
                info.DisplayName = "User";
                info.Group = "a Davids Columns";
                FieldUser userField = web.CreateField<FieldUser>(info);
                userField.SelectionMode = FieldUserSelectionMode.PeopleOnly;
                ctx.ExecuteQuery();

            }
          
            string isActiveid = "{AC949E82-2235-4EDB-A630-3D13C10EF28E}";

            if (!web.FieldExistsById(new Guid(isActiveid)))
            {
                FieldCreationInformation info = new FieldCreationInformation(FieldType.Boolean);
                info.Id = isActiveid.ToGuid();
                info.InternalName = "DAV_IsActive";
                info.DisplayName = "Is Active";
                info.Group = "a Davids Columns";
                web.CreateField(info);
            }


            web.AddFieldToContentTypeById(CVCT, pictureFieldId);
            web.AddFieldToContentTypeById(CVCT, userieldId);
            web.AddFieldToContentTypeById(CVCT, isActiveid);

            if (!web.ListExists("CVs"))
            {
                List list = web.CreateList(ListTemplateType.DocumentLibrary, "CVs", true, enableContentTypes: true);
                list.AddContentTypeToListById(CVCT); ;
            }

            List booksList = web.GetListByTitle("CVs");

            User user = web.EnsureUser("david@folkis2017.onmicrosoft.com");
            ctx.Load(user);
            ctx.ExecuteQuery();


            // adding a file to sharepoint
            FileCreationInformation fileInfo = new FileCreationInformation();
            System.IO.FileStream filestream = System.IO.File.OpenRead(@"C:\Users\david\source\repos\SP2017\OfficeDev1\ContentTypesAndFields\ContentTypesAndFields\file.txt");
            fileInfo.Content = ReadFully(filestream);
            fileInfo.Url = "file2.txt";
            Microsoft.SharePoint.Client.File file =  booksList.RootFolder.Files.Add(fileInfo);
            ctx.ExecuteQuery();

            ListItem item = file.ListItemAllFields;
            item["Title"] = "David";
            // set the content type.
            item["ContentTypeId"] = CVCT;
            // seeting a url or picture link
            FieldUrlValue picvalue = new FieldUrlValue();
            picvalue.Description = "david";
            picvalue.Url = "http://media.al.com/entertainment_impact/photo/TCDBAHA_FE002_H.JPG";
            item["DAV_Pic"] = picvalue;
            item["DAV_User"] = user.Id;
            item["DAV_IsActive"] = true;
           
            item.Update();
            ctx.ExecuteQueryRetry();

        }


        public static void RenameTitleFieldonCV(ClientContext ctx)
        {
            string CVCT = "0x0101000F34D58AC07F466082CFA31B4AF3B231";

            
            List list = ctx.Web.GetListByTitle("CVs");

            ContentType ct = list.GetContentTypeByName("CV");
            ctx.Load(ct.FieldLinks, flinks => flinks.Include(flink => flink.Name, flink => flink.DisplayName));
            ctx.ExecuteQuery();


         
            foreach (var fl in ct.FieldLinks)
            {
                //ctx.Load(fl);
                //ctx.ExecuteQuery();
                
                Console.WriteLine(fl.Name);
                Console.WriteLine(fl.DisplayName);
               

                if (fl.Name == "Title")
                {
                    fl.DisplayName = "CV Description";
                    ct.Update(false);
                    ctx.ExecuteQuery();
                }
            }

            Console.ReadLine();

            

        }

        public static void TurnOnMajorandMinorVersions(ClientContext ctx)
        {
           

            List list = ctx.Web.GetListByTitle("CVs");

            list.EnableVersioning = true;
            list.EnableMinorVersions = true;
            list.ForceCheckout = true;
            list.Update();
            ctx.ExecuteQuery();

           



        }

        public static void CreateView(ClientContext ctx)
        {


            List list = ctx.Web.GetListByTitle("CVs");

            ViewCreationInformation info = new ViewCreationInformation();
            info.ViewFields = new string[] { "Title", "DAV_User", "DAV_Pic", "DAV_IsActive" };
            info.Title = "Active CVs";
            info.Query = @"<Where><Eq><FieldRef Name='DAV_IsActive' /><Value Type='Integer'>1</Value></Eq></Where>"; 
            list.Views.Add(info);
            ctx.ExecuteQuery();





        }


        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
