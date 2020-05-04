using NewMvcHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMvcHand.Controllers
{
    public class BlogController : Controller
    {

        NarailDBEntities dBEntities = new NarailDBEntities();
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {

            var categories = dBEntities.Categories.ToList();
            var gallery = dBEntities.Galleries.ToList();
            var mixmodel = new ModelMix
            {
                Galleries = gallery,
                Categories = categories
            };

            return View(mixmodel);


            //  int tes = typeIdCount[0].Value;

            // var galleryCount = dBEntities. 

        }
        public ActionResult Popular()
        {
            return View();
        }
        public ActionResult Tag()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}