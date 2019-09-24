using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace registroPersonas.Models
{
    public class Persona
    {
        public int Cedula { get; set; }
        public int Codelec { get; set; }
        public int Sexo { get; set; }
        public DateTime fecha_caduc { get; set; }
        public int Junta { get; set; }
        public string Nombre { get; set; }
        public string Apellido1{ get; set; }
        public string Apellido2 { get; set; }
        private static OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);

        public static List<Persona> GetAll()
        {
            var Personas = new List<Persona>();
            connection.Open();

            string sqlQuery = "SELECT * from Personas";

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Persona tempPersona = new Persona
                {
                    Cedula = Convert.ToInt32(dr["Cedula"]),
                    Codelec = Convert.ToInt32(dr["Codelec"]),
                    Sexo = Convert.ToInt32(dr["Sexo"]),
                    fecha_caduc = Convert.ToDateTime(dr["fecha_caduc"]),
                    Junta = Convert.ToInt32(dr["Junta"]),
                    Nombre = Convert.ToString(dr["Nombre"]),
                    Apellido1= Convert.ToString(dr["Apellido1"]),
                    Apellido2 = Convert.ToString(dr["Apellido2"])
                };

                Personas.Add(tempPersona);
                //ViewBag.Personas = String.Format("{0} : {1}", dr["CEDULA"], dr["SEXO"]);
            }

            connection.Close();

            return Personas;
        }

        public static List<Persona> Search(Persona pPersona)
        {
            return new List<Persona>();
        }

        public static void Update(Persona pDistrito)
        {
        }
    }
}