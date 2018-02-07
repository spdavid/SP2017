using Microsoft.SharePoint.Client;
using MyFirstSharePointAddInWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstSharePointAddInWeb.Controllers
{
    public class ListFunController : Controller
    {
        // GET: ListFun
        public ActionResult Index(Boolean? isSuccess)
        {
            if (isSuccess.HasValue && isSuccess.Value)
            {
                ViewBag.Message = "your list is created. Good job";
            }



            return View();
        }

        [HttpPost]
        public ActionResult Index(string NewListTitle, string SPHostUrl)
        {
            try
            {

           
            using (ClientContext ctx = ContextHelper.GetContext())
            {
                ctx.Web.CreateList(ListTemplateType.GenericList, NewListTitle, false);
            }

            }
            catch (Exception ex)
            {
                throw ex;
               
            }

            return RedirectToAction("index",  new { SPHostUrl = SPHostUrl, isSuccess = true });


        }
    }
}