using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace registroPersonas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            //_log.Debug("Home page hit!");
            return View();
        }

        public ActionResult Search()
        {
            //ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Edit()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Person()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}