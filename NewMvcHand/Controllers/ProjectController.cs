using NewMvcHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMvcHand.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        NarailDBEntities dBEntities = new NarailDBEntities();
        public ActionResult Index()
        {
            var galleryList = dBEntities.Galleries.ToList();
            ViewBag.Title = "Proyektler | 2019";
            return View(galleryList);
        }
    }
}