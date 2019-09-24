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

        internal static dynamic Edit(string nombre, string apellido1, string apellido2, string cedula, string codelec)
        {
            Persona responsePersona = new Persona();
            connection.Open();

            string sqlQuery = string.Format("UPDATE Personas SET  nombre = '{0}', apellido1 = '{1}', apellido2 = '{1}', codelec = '{2}'", nombre.ToUpper(), apellido1.ToUpper(), apellido2.ToUpper(), codelec);

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            try
            {
                DataRow dr = dt.Rows[0];

                Persona tempPersona = new Persona
                {
                    Cedula = Convert.ToInt32(dr["Cedula"]),
                    Codelec = Convert.ToInt32(dr["Codelec"]),
                    Sexo = Convert.ToInt32(dr["Sexo"]),
                    fecha_caduc = Convert.ToDateTime(dr["fecha_caduc"]),
                    Junta = Convert.ToInt32(dr["Junta"]),
                    Nombre = Convert.ToString(dr["Nombre"]),
                    Apellido1 = Convert.ToString(dr["Apellido1"]),
                    Apellido2 = Convert.ToString(dr["Apellido2"])
                };

                responsePersona = tempPersona;
            }

            catch
            {
                Console.WriteLine("Not found");
            }

            connection.Close();
            return responsePersona;
        }
    

        public static Persona Search(string nombre, string apellido1, string apellido2)
        {
            Persona responsePersona = new Persona();
            connection.Open();

            string sqlQuery = string.Format("SELECT * from Personas WHERE nombre = '{0}' OR apellido1 = '{1}' OR apellido2 = '{1}'", nombre.ToUpper(), apellido1.ToUpper(), apellido2.ToUpper());

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            try
            {
                DataRow dr = dt.Rows[0];

                Persona tempPersona = new Persona
                {
                    Cedula = Convert.ToInt32(dr["Cedula"]),
                    Codelec = Convert.ToInt32(dr["Codelec"]),
                    Sexo = Convert.ToInt32(dr["Sexo"]),
                    fecha_caduc = Convert.ToDateTime(dr["fecha_caduc"]),
                    Junta = Convert.ToInt32(dr["Junta"]),
                    Nombre = Convert.ToString(dr["Nombre"]),
                    Apellido1 = Convert.ToString(dr["Apellido1"]),
                    Apellido2 = Convert.ToString(dr["Apellido2"])
                };

                responsePersona = tempPersona;
            }

            catch
            {
                Console.WriteLine("Not found");
            }

            connection.Close();
            return responsePersona;
        }

        public static void Update(Persona pDistrito)
        {
        }
    }
}