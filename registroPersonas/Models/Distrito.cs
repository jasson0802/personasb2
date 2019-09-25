using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace registroPersonas.Models
{
    public class Distrito
    {
        public int Codelec { get; set; }
        public int IDProvincia { get; set; }
        public int IDCanton{ get; set; }
        public string Nombre { get; set; }
        private static OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);

        public static List<Distrito> GetAll()
        {
            var Distritos = new List<Distrito>();
            connection.Open();

            string sqlQuery = "SELECT * from DISTRITOS";

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Distrito tempDistrito = new Distrito {
                    Codelec = Convert.ToInt32(dr["codelec"]),
                    IDProvincia = Convert.ToInt32(dr["id_provincia"]),
                    IDCanton = Convert.ToInt32(dr["id_canton"]),
                    Nombre = Convert.ToString(dr["Nombre"])
                };

                Distritos.Add(tempDistrito);
                //ViewBag.Personas = String.Format("{0} : {1}", dr["CEDULA"], dr["SEXO"]);
            }

            connection.Close();

            return Distritos;
        }

        public static List<Distrito> GetByCanton(int idCanton)
        {
            return new List<Distrito>();
        }

        public static void Update(Distrito pDistrito)
        {
        }
    }
}