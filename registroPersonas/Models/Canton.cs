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
    public class Canton
    {
        public int IDCanton { get; set; }
        public string Nombre { get; set; }
        public int IDProvincia { get; set; }
        private static OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);

        public static List<Canton> GetAll()
        {
            var Cantones = new List<Canton>();
            connection.Open();

            string sqlQuery = "SELECT * from Cantones";

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Canton tempCanton = new Canton
                {
                    IDProvincia = Convert.ToInt32(dr["id_provincia"]),
                    IDCanton = Convert.ToInt32(dr["id_canton"]),
                    Nombre = Convert.ToString(dr["nombre"])
                };

                Cantones.Add(tempCanton);
                //ViewBag.Personas = String.Format("{0} : {1}", dr["CEDULA"], dr["SEXO"]);
            }

            connection.Close();

            return Cantones;
        }

        public static List<Canton> GetByProvincia(int idProvincia)
        {
            return new List<Canton>();
        }

        public static void Update(Canton pCanton)
        {
        }
    }
}