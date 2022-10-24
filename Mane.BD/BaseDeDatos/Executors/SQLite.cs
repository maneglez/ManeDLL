using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.Executors
{
    internal class SQLite : IBdExecutor
    {
        private SQLiteConnection conn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;
        internal SQLite(string query, string connString)
        {
            Query = query;
            ConnString = connString;
        }

        public string Query { get; set; }
        public string ConnString { get; set; }

        public SQLite(string connString)
        {
            ConnString = connString;
        }

        public void Connect()
        {
            if (conn?.State == ConnectionState.Open)
                Disconnect();
            try
            {
                conn = new SQLiteConnection(ConnString);
                cmd = conn.CreateCommand();
                cmd.CommandText = Query;
                conn.Open();
            }
            catch (SQLiteException ex)
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
            catch (SQLiteException ex)
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
            catch (SQLiteException ex)
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
            catch (SQLiteException ex)
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
                conn = new SQLiteConnection(ConnString);
                conn.Open();
            }
            catch (SQLiteException)
            {
                return false;
            }
            return true;
        }
    }
}
