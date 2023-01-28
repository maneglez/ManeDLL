using System.Data;
using System.Data.SQLite;

namespace Mane.BD.Executors
{
    internal class SQLiteExecutor : IBdExecutor
    {
        private SQLiteConnection conn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;
        internal SQLiteExecutor(string query, string connString)
        {
            Query = query;
            ConnString = connString;
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

        public SQLiteExecutor(string connString, bool autodisconnect = true)
        {
            ConnString = connString;
            AutoDisconnect = autodisconnect;
        }

        public void Connect()
        {
            try
            {
                if (conn?.State == ConnectionState.Open)
                    Disconnect();
                else
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
            catch (SQLiteException ex)
            {
                Bd.bdExceptionHandler(ex, Query);
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
                Bd.bdExceptionHandler(ex, Query);
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
                Bd.bdExceptionHandler(ex, Query);
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
