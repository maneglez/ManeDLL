using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public static partial class Bd
    {
        private static DataTable executeQueryForSQLServer(string query, string nombreConexion = "")
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (SqlException e)
            {
                bdExceptionHandler(e, query);
            }
            catch (Exception e)
            {
                bdExceptionHandler(e, query);
            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return dt;
        }
        private static int executeNonQueryForSQLServer(string query, string nombreConexion = "")
        {
            int result = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                bdExceptionHandler(e, query);
            }
            catch (Exception e)
            {
                bdExceptionHandler(e, query);

            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string toDateTimeSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd HH:mm:ss.fff");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string toDateSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd");

        private static object executeScalarSQL(string query, string nombreConexion)
        {


            object result = null;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
                result = cmd.ExecuteScalar();
            }
            catch (SqlException sqlE)
            {
                bdExceptionHandler(sqlE, query);
            }
            catch (Exception e)
            {
                bdExceptionHandler(e, query);
            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return result;
        }

    }
}
