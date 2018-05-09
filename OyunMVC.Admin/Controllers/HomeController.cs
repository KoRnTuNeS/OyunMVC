using OyunMVC.Admin.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OyunMVC.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [LoginFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}