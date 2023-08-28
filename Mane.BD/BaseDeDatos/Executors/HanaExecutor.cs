using System;
using System.Data;
using System.Data.Odbc;

namespace Mane.BD.Executors
{
    public class HanaExecutor : IBdExecutor
    {
        private OdbcConnection conn = null;
        private OdbcCommand cmd = null;
        private OdbcDataReader reader = null;
        public HanaExecutor(string connString, bool autoDisconnect = true)
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
            catch (Exception ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
        }

        public void Disconnect()
        {
            //if (!AutoDisconnect) return;
            reader?.Close();
            cmd?.Dispose();
            conn?.Close();
            conn?.Dispose();
        }

        public object ExecuteEscalar()
        {
            Connect();
            object result = null;
            try
            {
                result = cmd.ExecuteScalar();
            }
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
            catch (Exception ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
            Disconnect();
            return result;
        }

        public int ExecuteNonQuery()
        {
            Connect();
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
            catch (Exception ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
            Disconnect();
            return result;
        }

        public DataTable ExecuteQuery()
        {
            var dt = new DataTable();
            Connect();
            try
            {
                using (var adapter = new OdbcDataAdapter(cmd))
                    adapter.Fill(dt);
            }
            catch (OdbcException ex)
            {
                Bd.bdExceptionHandler(ex, Query);
            }
            catch(Exception ex)
            {
                Bd.bdExceptionHandler(ex, Query);
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
            catch (OdbcException e)
            {
                Bd.LastErrorCode = e.ErrorCode;
                Bd.LastErrorDescription = e.Message;
                return false;
            }
            catch (Exception ex)
            {
                Bd.LastErrorCode = 0;
                Bd.LastErrorDescription = ex.Message;
                return false;
            }
            Disconnect();
            return true;
        }
    }
}
