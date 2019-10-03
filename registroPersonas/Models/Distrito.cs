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
            var database = System.Web.HttpContext.Current.Session["database"] as String;
            string sqlQuery = "SELECT * from DISTRITOS";

            var Distritos = new List<Distrito>();

            if (database == "Oracle")
            {
                connection.Open();


                OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
                DataTable dt = new DataTable();
                oda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Distrito tempDistrito = new Distrito
                    {
                        Codelec = Convert.ToInt32(dr["codelec"]),
                        IDProvincia = Convert.ToInt32(dr["id_provincia"]),
                        IDCanton = Convert.ToInt32(dr["id_canton"]),
                        Nombre = Convert.ToString(dr["Nombre"])
                    };

                    Distritos.Add(tempDistrito);
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
                                Distrito tempDistrito = new Distrito();

                                tempDistrito.Codelec = int.Parse(dr.GetDecimal(0).ToString());
                                tempDistrito.IDProvincia = int.Parse(dr.GetByte(1).ToString());
                                tempDistrito.IDCanton = int.Parse(dr.GetByte(2).ToString());
                                tempDistrito.Nombre = dr.GetString(3);

                                Distritos.Add(tempDistrito);
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

            return Distritos;
        }

        public static List<Distrito> GetByCanton(int idCanton)
        {
            return new List<Distrito>();
        }

        public static List<Distrito> GetByCodelec(int codelec)
        {
            var database = System.Web.HttpContext.Current.Session["database"] as String;
            string sqlQuery = string.Format("SELECT * from DISTRITOS WHERE codelec = {0}", codelec);

            var Distritos = new List<Distrito>();

            if (database == "Oracle")
            {
                connection.Open();


                OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);
                DataTable dt = new DataTable();
                oda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Distrito tempDistrito = new Distrito
                    {
                        Codelec = Convert.ToInt32(dr["codelec"]),
                        IDProvincia = Convert.ToInt32(dr["id_provincia"]),
                        IDCanton = Convert.ToInt32(dr["id_canton"]),
                        Nombre = Convert.ToString(dr["Nombre"])
                    };

                    Distritos.Add(tempDistrito);
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
                                Distrito tempDistrito = new Distrito();

                                tempDistrito.Codelec = int.Parse(dr.GetDecimal(0).ToString());
                                tempDistrito.IDProvincia = int.Parse(dr.GetByte(1).ToString());
                                tempDistrito.IDCanton = int.Parse(dr.GetByte(2).ToString());
                                tempDistrito.Nombre = dr.GetString(3);

                                Distritos.Add(tempDistrito);
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

            return Distritos;
        }

        public static void Update(Distrito pDistrito)
        {
        }
    }
}