﻿using System.Data;
using System.Data.SqlClient;

namespace Mane.BD.Executors
{
    internal class SQLServerExecutor : IBdExecutor
    {
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;
        public SQLServerExecutor(string connString, bool autodisconnect = true)
        {
            ConnString = connString;
            AutoDisconnect = autodisconnect;
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
            catch (SqlException ex)
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
            catch (SqlException ex)
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
