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
    public class BlogAdminController : Controller
    {
        // GET: BlogAdmin
        NarailDBEntities dBEntities = new NarailDBEntities();
        public ActionResult Index()
        {
            var galleries = dBEntities.Galleries.ToList();
            return View(galleries);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Gallery gallery = dBEntities.Galleries.Find(id);
            dBEntities.Galleries.Remove(gallery);
            dBEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            Gallery gallery = dBEntities.Galleries.Find(Id);
            return PartialView(gallery);
        }

        public ActionResult Create()
        {
            var categories = dBEntities.Categories.ToList();

            var mixmodel = new ModelMix
            {

                Categories = categories
            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult Create(Gallery gallery, HttpPostedFileBase File)
        {
            var galeryExist = dBEntities.Galleries.ToList();



            if (galeryExist == null)
            {
                gallery.AddedDate = DateTime.Now;
                gallery.AddedBy = "Sheyx Hacibayli";

                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string tamyol = "~/images/gallery/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    gallery.İmage = "/images/gallery/" + uzanti;

                }

                dBEntities.Galleries.Add(gallery);
                dBEntities.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            Gallery gallery = dBEntities.Galleries.Find(Id);
            return View(gallery);
        }
        [HttpPost]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase File)
        {
            if (gallery != null)
            {

                var url = Url.RequestContext.RouteData.Values["id"];
                int id = Convert.ToInt32(url);
                gallery.İd = id;
                dBEntities.Entry(gallery).State = System.Data.Entity.EntityState.Modified;
                dBEntities.Entry(gallery).Property(m => m.AddedBy).IsModified = false;
                dBEntities.Entry(gallery).Property(m => m.AddedDate).IsModified = false;

                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string tamyol = "~/images/gallery/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    gallery.İmage = "/images/gallery/" + uzanti;
                }
                else
                {
                    dBEntities.Entry(gallery).Property(m => m.İmage).IsModified = false;
                }
                gallery.AddedBy = "Sheyx Hacibayli";
                gallery.ModifyDate = DateTime.Now;
            }
            dBEntities.SaveChanges();
            return RedirectToAction("Index", "BlogAdmin");
        }
    }
}