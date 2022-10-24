using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.Executors
{
    internal class SQLServer : IBdExecutor
    {
      private SqlConnection conn = null;
      private SqlCommand cmd = null;
      private SqlDataReader reader = null;
        internal SQLServer(string query, string connString)
        {
            Query = query;
            ConnString = connString;
        }
        public SQLServer(string connString)
        {
            ConnString = connString;
        }


        public string Query { get; set; }
        public string ConnString { get; set; }

        

        public void Connect()
        {
            if (conn?.State == ConnectionState.Open)
                Disconnect();
            try
            {
                conn = new SqlConnection(ConnString);
                cmd = conn.CreateCommand();
                cmd.CommandText = Query;
                conn.Open();
            }
            catch (SqlException ex)
            {
                Bd.bdExceptionHandler(ex);
            }
        }

        public void Disconnect()
        {
            reader?.Close();
            cmd?.Dispose();
            conn?.Close();
            conn?.Dispose();
        }

        public object ExecuteEscalar()
        {
            Connect();
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Bd.bdExceptionHandler(ex);
            }
            Disconnect();
            return null;
        }

        public int ExecuteNonQuery()
        {
            Connect();
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Bd.bdExceptionHandler(ex);
            }
            Disconnect();
            return 0;
        }

        public DataTable ExecuteQuery()
        {
            var dt = new DataTable();
            Connect();
            try
            {
               reader = cmd.ExecuteReader();
               dt.Load(reader);
            }
            catch (SqlException ex)
            {
                Bd.bdExceptionHandler(ex);
            }
            Disconnect();
            return dt;
        }

        public bool TestConnection()
        {
            try
            {
                conn = new SqlConnection(ConnString);
                conn.Open();
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }
    }
}
