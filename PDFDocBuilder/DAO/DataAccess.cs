using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PDFDocBuilder.DAO
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// Singleton class for database interactions.
    /// </summary>
    public class DataAccess
    {
        private static DataAccess _instance;
        private String conn_string;

        private DataAccess()
        {
            conn_string = ConfigurationManager.ConnectionStrings["AVIF"].ConnectionString;
        }

        public static DataAccess getInstance()
        {
            if (_instance == null)
            {
                _instance = new DataAccess();
            }
            return _instance;
        }

        /// <summary>
        /// Takes the non-query statement and executes it. IE insert, truncate.
        /// </summary>
        /// <param name="cmd">Command to execute</param>
        public void ExecuteNonQuery(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                // Get the query
                using (cmd)
                {
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed to execute statement.\nDetails: " + e.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Takes a query, executes it and returns the result set.
        /// </summary>
        /// <param name="cmd">Selct query with parameters set.</param>
        /// <returns>Datatable representing the returnset.</returns>
        public DataTable ExecuteSelect(SqlCommand cmd) 
        {
            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                conn.Open();
                DataTable dt = new DataTable();

                // Get the query
                using (cmd)
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            da.Fill(dt);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Failed to execute SELECT statement.\nDetails: " + e.Message);
                        }
                        finally
                        {
                            cmd.Dispose();
                            da.Dispose();
                        }
                    }
                }
                return dt;
            }
        }
    }
}
