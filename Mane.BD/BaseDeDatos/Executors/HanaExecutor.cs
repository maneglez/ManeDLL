using Mane.BD.Executors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.Executors
{
    public class HanaExecutor : IBdExecutor
    {
        private OdbcConnection conn = null;
        private OdbcCommand cmd = null;
        private OdbcDataReader reader = null;
        public HanaExecutor(string connString, bool autoDisconnect)
        {
            ConnString = connString;
            AutoDisconnect = autoDisconnect;
        }
        public string Query { get; set; }
        public string ConnString { get; set; }
        public bool AutoDisconnect
        {
            get => autoDisconnect; set
            {
                if (autoDisconnect == value) return;
                autoDisconnect = value;
                if (value) Disconnect();
            }
        }
        private bool autoDisconnect;


        public void Connect()
        {
            try
            {
                if (conn?.State == ConnectionState.Open)
                    Disconnect();
                else
                    conn = new OdbcConnection(ConnString);
                cmd = conn.CreateCommand();
                cmd.CommandText = Query;
                conn.Open();
            }
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex);
            }
        }

        public void Disconnect()
        {
            if (!AutoDisconnect) return;
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
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex,Query);
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
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex,Query);
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
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex,Query);
            }
            Disconnect();
            return dt;
        }

        public bool TestConnection()
        {
            if (conn?.State == ConnectionState.Open)
                Disconnect();
            try
            {
                conn = new OdbcConnection(ConnString);
                conn.Open();
            }
            catch (OdbcException)
            {
                return false;
            }
            return true;
        }
    }
}
