using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meetup.Websites.Controllers
{
    public class HomeController: Controller
    {
        /// <summary>
        /// An action returning the home page
        /// </summary>
        /// <returns>Returns the home page</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}