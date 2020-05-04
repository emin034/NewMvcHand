using NewMvcHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandCraft.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "ƏLiSH | 2019";
            return View();
        }
        public ActionResult Slider()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(UserRequest userRequest)
        {
            NarailDBEntities dBEntities = new NarailDBEntities();
            var user = dBEntities.UserRequests.Any(m => m.Email == userRequest.Email);

            if (user == false)
            {
                userRequest.SendDate = DateTime.Now;

                dBEntities.UserRequests.Add(userRequest);
                dBEntities.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult Counter()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
    }
}