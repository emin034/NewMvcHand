using NewMvcHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMvcHand.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        NarailDBEntities db = new NarailDBEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}