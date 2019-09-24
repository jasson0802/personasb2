using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using registroPersonas.Models;

namespace registroPersonas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));
        
    public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            //ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Provincias = Provincia.GetAll();
            ViewBag.Cantones = Canton.GetAll();
            ViewBag.Distritos = Distrito.GetAll();
            return View();
        }

        public ActionResult Person(string nombre, string apellido1, string apellido2)
        {
            ViewBag.Personas = Persona.Search(ViewBag.Persona);
            
            return View("Person");
        }
    }
}