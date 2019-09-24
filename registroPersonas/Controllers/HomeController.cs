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

        public ActionResult Search(string nombre, string apellido1, string apellido2)
        {
            //ViewBag.Message = "Your application description page.";
            

            return View();
        }

        public ActionResult Edit(string cedula)
        {
            if (!string.IsNullOrEmpty(cedula))
            {
                ViewBag.Persona = Persona.GetByCedula(cedula);   
            }
            else
            {
                Persona tempPersona = new Persona
                {
                    Nombre = "",
                    Apellido1 = "",
                    Apellido2 = "",
                    Cedula = 0,
                    Codelec = 0
                };
                ViewBag.Persona = tempPersona;
            }

            ViewBag.Provincias = Provincia.GetAll();
            ViewBag.Cantones = Canton.GetAll();
            ViewBag.Distritos = Distrito.GetAll();

            return View();
        }

        public ActionResult Person(string nombre, string apellido1, string apellido2)
        {
            Persona tempPersona = new Persona
            {
                Nombre = "",
                Apellido1 = "",
                Apellido2 = "",
                Cedula = 0,
                Codelec = 0
            };

            if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(apellido1) && string.IsNullOrEmpty(apellido2))
            {
                ViewBag.Persona = tempPersona;
            }

            else
            {
                ViewBag.Persona = Persona.Search(nombre, apellido1, apellido2);
            }
            
            return View("Person");
        }

        public ActionResult EditResult(string nombre, string apellido1, string apellido2, string cedula, string codelec)
        {
            Persona tempPersona = new Persona
            {
                Nombre = "",
                Apellido1 = "",
                Apellido2 = "",
                Cedula = 0,
                Codelec = 0
            };

            if (!string.IsNullOrEmpty(cedula))
            {
                ViewBag.Persona = Persona.Edit(nombre, apellido1, apellido2, cedula, codelec);
            }
            else {
                ViewBag.Persona = tempPersona;
            }

            return View("EditResult");
        }
    }
}