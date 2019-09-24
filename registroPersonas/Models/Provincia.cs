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
    public class Provincia
    {
        public int IDProvincia;
        public string Nombre;
        private static OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);

        public static List<Provincia> GetAll()
        {
            var Provincias = new List<Provincia>();
            connection.Open();

            string sqlQuery = "SELECT * from Provincias";

            OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Provincia tempProvincia = new Provincia
                {
                    IDProvincia = Convert.ToInt32(dr["id_provincia"]),
                    Nombre = Convert.ToString(dr["nombre"])
                };

                Provincias.Add(tempProvincia);
                //ViewBag.Personas = String.Format("{0} : {1}", dr["CEDULA"], dr["SEXO"]);
            }

            connection.Close();

            return Provincias;
        }
    }
}