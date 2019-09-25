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

        public static Persona GetByCedula(string cedula)
        {
            var Personas = new List<Persona>();
            var database = System.Web.HttpContext.Current.Session["database"] as String;
            string sqlQuery = string.Format("SELECT * from Personas WHERE cedula = '{0}'", cedula);
            
            if (database == "Oracle")
            {
                connection.Open();
                
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
                        Apellido1 = Convert.ToString(dr["Apellido1"]),
                        Apellido2 = Convert.ToString(dr["Apellido2"])
                    };

                    Personas.Add(tempPersona);
                }

                connection.Close();
            }

            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PadronConnectionString"].ConnectionString))
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
                                Persona tempPersona = new Persona();
                                
                                tempPersona.Cedula = int.Parse(dr.GetDecimal(0).ToString());
                                tempPersona.Codelec = int.Parse(dr.GetDecimal(1).ToString());
                                tempPersona.Sexo = int.Parse(dr.GetDecimal(2).ToString());
                                tempPersona.fecha_caduc = dr.GetDateTime(3);
                                tempPersona.Junta = int.Parse(dr.GetDecimal(4).ToString());
                                tempPersona.Nombre = dr.GetString(5);
                                tempPersona.Apellido1 = dr.GetString(6);
                                tempPersona.Apellido2 = dr.GetString(7);
                                
                                Personas.Add(tempPersona);
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
            
            return Personas.FirstOrDefault();
        }

        internal static dynamic Edit(string nombre, string apellido1, string apellido2, string cedula, string codelec)
        {
            Persona responsePersona = new Persona();
            var database = System.Web.HttpContext.Current.Session["database"] as String;
            string sqlQuery = string.Format("SELECT * FROM Personas WHERE cedula = '{0}'", cedula);

            if (database == "Oracle")
            {
                connection.Open();

                
                OracleDataAdapter oda = new OracleDataAdapter(sqlQuery, connection);

                OracleCommandBuilder builder = new OracleCommandBuilder(oda);

                DataTable dt = new DataTable();
                oda.Fill(dt);

                dt.Columns["cedula"].Unique = true;

                DataRow row = dt.Rows[0];

                row["nombre"] = nombre;
                row["apellido1"] = apellido1;
                row["apellido2"] = apellido2;
                row["codelec"] = codelec;

                oda.Update(dt);

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
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PadronConnectionString"].ConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("UPDATE Personas SET codelec = @codelec, nombre = @nombre, apellido1 = @apellido1, apellido2 = @apellido2 WHERE cedula = @cedula", conn);

                    sqlCmd.Parameters.Add("@codelec", SqlDbType.Int);
                    sqlCmd.Parameters["@codelec"].Value = codelec;

                    sqlCmd.Parameters.Add("@nombre", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@nombre"].Value = nombre;

                    sqlCmd.Parameters.Add("@apellido1", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@apellido1"].Value = apellido1;

                    sqlCmd.Parameters.Add("@apellido2", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@apellido2"].Value = apellido2;

                    sqlCmd.Parameters.Add("@cedula", SqlDbType.Int);
                    sqlCmd.Parameters["@cedula"].Value = cedula;

                    try
                    {
                        List<Persona> Personas = new List<Persona>();
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                        conn.Close();

                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        conn.Open();

                        //execute the SQLCommand
                        SqlDataReader dr = cmd.ExecuteReader();

                        //check if there are records
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Persona tempPersona = new Persona();

                                tempPersona.Cedula = int.Parse(dr.GetDecimal(0).ToString());
                                tempPersona.Codelec = int.Parse(dr.GetDecimal(1).ToString());
                                tempPersona.Sexo = int.Parse(dr.GetDecimal(2).ToString());
                                tempPersona.fecha_caduc = dr.GetDateTime(3);
                                tempPersona.Junta = int.Parse(dr.GetDecimal(4).ToString());
                                tempPersona.Nombre = dr.GetString(5);
                                tempPersona.Apellido1 = dr.GetString(6);
                                tempPersona.Apellido2 = dr.GetString(7);

                                Personas.Add(tempPersona);
                            }

                            responsePersona = Personas.FirstOrDefault();
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
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("SQL ERROR: " + sqlEx.ErrorCode);
                    }
                }
            }

            return responsePersona;
        }
    

        public static Persona Search(string nombre, string apellido1, string apellido2)
        {
            Persona responsePersona = new Persona();
            var database = System.Web.HttpContext.Current.Session["database"] as String;
            string sqlQuery = string.Format("SELECT * from Personas WHERE nombre = '{0}' OR apellido1 = '{1}' OR apellido2 = '{1}'", nombre.ToUpper(), apellido1.ToUpper(), apellido2.ToUpper());

            if (database == "Oracle")
            {
                connection.Open();
                
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
                    Persona tempPersona = new Persona
                    {
                        Cedula = 0,
                        Codelec = 0,
                        Sexo = 0,
                        fecha_caduc = new DateTime(),
                        Junta = 0,
                        Nombre = "",
                        Apellido1 = "",
                        Apellido2 = ""
                    };

                    responsePersona = tempPersona;
                }

                connection.Close();
            }

            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PadronConnectionString"].ConnectionString))
                    {
                        List<Persona> Personas = new List<Persona>();
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        conn.Open();

                        //execute the SQLCommand
                        SqlDataReader dr = cmd.ExecuteReader();

                        //check if there are records
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Persona tempPersona = new Persona();

                                tempPersona.Cedula = int.Parse(dr.GetDecimal(0).ToString());
                                tempPersona.Codelec = int.Parse(dr.GetDecimal(1).ToString());
                                tempPersona.Sexo = int.Parse(dr.GetDecimal(2).ToString());
                                tempPersona.fecha_caduc = dr.GetDateTime(3);
                                tempPersona.Junta = int.Parse(dr.GetDecimal(4).ToString());
                                tempPersona.Nombre = dr.GetString(5);
                                tempPersona.Apellido1 = dr.GetString(6);
                                tempPersona.Apellido2 = dr.GetString(7);
                               

                                Personas.Add(tempPersona);
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

                        responsePersona = Personas.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
            
            return responsePersona;
        }

        public static void Update(Persona pDistrito)
        {
        }
    }
}