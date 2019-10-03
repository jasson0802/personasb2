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
            string sqlQuery = "SELECT * from Cantones";

            var database = System.Web.HttpContext.Current.Session["database"] as String;

            var Cantones = new List<Canton>();

            if (database == "Oracle")
            {
                connection.Open();


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
                                Canton tempCanton = new Canton();

                                tempCanton.IDCanton = int.Parse(dr.GetByte(0).ToString());
                                tempCanton.Nombre = dr.GetString(1);
                                tempCanton.IDProvincia = int.Parse(dr.GetByte(2).ToString());

                                Cantones.Add(tempCanton);
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

            return Cantones;
        }

        public static List<Canton> GetByProvincia(int idProvincia)
        {
            string sqlQuery = string.Format("SELECT * from Cantones WHERE id_provincia = {0}", idProvincia);

            var database = System.Web.HttpContext.Current.Session["database"] as String;

            var Cantones = new List<Canton>();

            if (database == "Oracle")
            {
                connection.Open();


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
                                Canton tempCanton = new Canton();

                                tempCanton.IDCanton = int.Parse(dr.GetByte(0).ToString());
                                tempCanton.Nombre = dr.GetString(1);
                                tempCanton.IDProvincia = int.Parse(dr.GetByte(2).ToString());

                                Cantones.Add(tempCanton);
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

            return Cantones;
        }

        public static void Update(Canton pCanton)
        {
        }
    }
}