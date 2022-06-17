using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ti.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Zwraca widok glowny
        /// </summary>
        /// /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}