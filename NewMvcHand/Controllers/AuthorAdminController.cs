using NewMvcHand.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NewMvcHand.Controllers
{
    public class AuthorAdminController : Controller
    {
        NarailDBEntities dBEntities = new NarailDBEntities();
        public ActionResult Index()
        {
            return View(dBEntities.Authors.ToList());
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            Author author = dBEntities.Authors.Find(Id);
            dBEntities.Authors.Remove(author);
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Author author, HttpPostedFileBase File)
        {
            var authorExist = dBEntities.Authors.Any(m => m.Email == author.Email);

            if (authorExist == false)
            {
                author.AddedDate = DateTime.Now;
                author.AddedBy = "Sheyx Hacibayli";
                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string tamyol = "~/images/users/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    author.Image = "/images/users/" + uzanti;
                }
                dBEntities.Authors.Add(author);
                dBEntities.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            Author author = dBEntities.Authors.Find(Id);
            return PartialView(author);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            Author author = dBEntities.Authors.Find(Id);
            return View(author);
        }
        [HttpPost]
        public ActionResult Edit(Author author, HttpPostedFileBase File)
        {
            if (author != null)
            {

                var url = Url.RequestContext.RouteData.Values["id"];
                int id = Convert.ToInt32(url);
                author.İd = id;
                dBEntities.Entry(author).State = System.Data.Entity.EntityState.Modified;
                dBEntities.Entry(author).Property(m => m.AddedBy).IsModified = false;
                dBEntities.Entry(author).Property(m => m.AddedDate).IsModified = false;

                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string tamyol = "~/Content/images/users/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    author.Image = "/images/users/" + uzanti;
                }
                else
                {
                    dBEntities.Entry(author).Property(m => m.Image).IsModified = false;
                }
                author.ModifyBy = "Sheyx Hacibayli";
                author.ModifyDate = DateTime.Now;
            }
            dBEntities.SaveChanges();
            return RedirectToAction("Index", "AuthorAdmin");
        }
    }
}