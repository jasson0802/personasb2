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
    public class Provincia
    {
        public int IDProvincia;
        public string Nombre;
        private static OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);

        public static List<Provincia> GetAll()
        {
            var Provincias = new List<Provincia>();

            var database = System.Web.HttpContext.Current.Session["database"] as String;

            string sqlQuery = "SELECT * from Provincias";
            
            if (database == "Oracle") {
                try {
                                connection.Open();

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
                }
                catch {
                    return Provincias;
                }
            }

            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ClusterConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        conn.Open();

                        //execute the SQLCommand
                        SqlDataReader dr = cmd.ExecuteReader();

                        //check if there are records
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Provincia tempProvincia = new Provincia();

                                var datatype = dr.GetDataTypeName(0);
                                tempProvincia.IDProvincia = dr.GetByte(0);
                                tempProvincia.Nombre = dr.GetString(1);

                                Provincias.Add(tempProvincia);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found.");
                        }

                        //close data reader
                        dr.Close();

                        //close connection
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    //display error message
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            return Provincias;
        }
    }
}