using Microsoft.SharePoint.Client;
using MyFirstSharePointAddInWeb.Helpers;
using MyFirstSharePointAddInWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstSharePointAddInWeb.Controllers
{
    public class SPListsController : Controller
    {
        // GET: SPLists
        public ActionResult Index()
        {
          List<SPList> splists = new List<SPList>();
            using (ClientContext ctx = ContextHelper.GetContext())
            {
                if (ctx != null)
                {
                    ListCollection lists = ctx.Web.Lists;
                    ctx.Load(lists, 
                        lsts => lsts.Include(l => l.Id),
                        lsts => lsts.Include(l => l.Title),
                        lsts => lsts.Include(l => l.DefaultViewUrl));
                    ctx.ExecuteQuery();
                    foreach(List list in lists)
                    {
                        splists.Add(new SPList() { ListId = list.Id, Title = list.Title, Url = list.DefaultViewUrl });
                    }
                }
            }
                    return View(splists);
        }

        // GET: SPLists/Details/5
        public ActionResult Details(string listId)
        {
            using (ClientContext ctx = ContextHelper.GetContext())
            {
               List list =  ctx.Web.GetListById(listId.ToGuid());
                SPList spList = new SPList();
                spList.ListId = list.Id;
                spList.Title = list.Title;
                spList.Url = list.DefaultViewUrl;

                return View(spList);
            }
                
        }

        //// GET: SPLists/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: SPLists/Create
        [HttpPost]
        public ActionResult Create(SPList spList, string SPHostUrl)
        {
            try
            {
                using (ClientContext ctx = ContextHelper.GetContext())
                {
                    ctx.Web.CreateList(ListTemplateType.GenericList, spList.Title, false, urlPath: spList.Url);
                }

                    return RedirectToAction("Index", new { SPHostUrl = SPHostUrl });
            }
            catch
            {
                return View();
            }
        }

        // GET: SPLists/Edit/5
        public ActionResult Edit(string listId)
        {
            using (ClientContext ctx = ContextHelper.GetContext())
            {
                List list = ctx.Web.GetListById(listId.ToGuid());
                SPList spList = new SPList();
                spList.ListId = list.Id;
                spList.Title = list.Title;
                spList.Url = list.DefaultViewUrl;

                return View(spList);
            }
        }

        // POST: SPLists/Edit/5
        [HttpPost]
        public ActionResult Edit(SPList spList, string SPHostUrl)
        {
            try
            {
                // TODO: Add update logic here
                using (ClientContext ctx = ContextHelper.GetContext())
                {
                    List list = ctx.Web.GetListById(spList.ListId);
                    list.Title = spList.Title;
                    list.Update();
                    ctx.ExecuteQuery();
                }

                    return RedirectToAction("Index", new { SPHostUrl = SPHostUrl });
            }
            catch
            {
                return View();
            }
        }

        // GET: SPLists/Delete/5
        public ActionResult Delete(string listId)
        {
            using (ClientContext ctx = ContextHelper.GetContext())
            {
                List list = ctx.Web.GetListById(listId.ToGuid());
                SPList spList = new SPList();
                spList.ListId = list.Id;
                spList.Title = list.Title;
                spList.Url = list.DefaultViewUrl;

                return View(spList);
            }
        }

        // POST: SPLists/Delete/5
        [HttpPost]
        public ActionResult Delete(string listId, string SPHostUrl)
        {
            try
            {
                using (ClientContext ctx = ContextHelper.GetContext())
                {
                    List list = ctx.Web.GetListById(listId.ToGuid());
                    list.DeleteObject();
                    ctx.ExecuteQuery();
                }

                    return RedirectToAction("Index", new { SPHostUrl=SPHostUrl });
            }
            catch
            {
                return View();
            }
        }
    }
}
