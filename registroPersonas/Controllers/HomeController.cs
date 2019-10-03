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

        public ActionResult Search(string database)
        {
            //ViewBag.Message = "Your application description page.";

            System.Web.HttpContext.Current.Session["database"] = database;

            return View();
        }

        public ActionResult Edit(string cedula)
        {
            Persona ControllerPerson = new Persona();

            if (!string.IsNullOrEmpty(cedula))
            {
                ControllerPerson = Persona.GetByCedula(cedula);
                ViewBag.Persona = ControllerPerson;
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

                ControllerPerson = tempPersona;
                ViewBag.Persona = ControllerPerson;
            }

            var tempCodelec = ControllerPerson.Codelec.ToString();
            var tempDistrito = Distrito.GetByCodelec(ControllerPerson.Codelec);

            var tempProvincia = 1;
            if (ControllerPerson.Codelec != 0)
            {
                try
                {
                    tempProvincia = tempDistrito.FirstOrDefault().IDProvincia;
                    var tempCantones = Canton.GetByProvincia(tempProvincia);

                    ViewBag.Provincias = Provincia.GetAll();
                    ViewBag.Cantones = tempCantones;
                    ViewBag.Distritos = tempDistrito;
                    ViewBag.SelectedProvincia = tempDistrito.FirstOrDefault().IDProvincia;
                    ViewBag.SelectedCanton = tempDistrito.FirstOrDefault().IDCanton;
                }
                catch
                {
                    Console.WriteLine("Cannot get provincia from codelec");
                }
            }

            else
            {
                ViewBag.Provincias = new List<Provincia>();
                ViewBag.Cantones = new List<Canton>();
                ViewBag.Distritos = new List<Distrito>();
                ViewBag.SelectedProvincia = 0;
                ViewBag.SelectedCanton = 0;
            }

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

        public ActionResult EditResult(string nombre, string apellido1, string apellido2, string cedula, string codelec, string sexo, string junta)
        {
            Persona tempPersona = new Persona
            {
                Nombre = "",
                Apellido1 = "",
                Apellido2 = "",
                Cedula = 0,
                Codelec = 0,
                Sexo = 0,
                Junta = 0
            };

            if (!string.IsNullOrEmpty(cedula))
            {
                ViewBag.Persona = Persona.Edit(nombre, apellido1, apellido2, cedula, codelec, sexo, junta);
            }
            else {
                ViewBag.Persona = tempPersona;
            }

            return View("EditResult");
        }
    }
}