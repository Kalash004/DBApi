﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject
{
    internal class DataBaseConnection
    {
        private SqlConnection? conn = null;
        public DataBaseConnection()
        {
        }

        public SqlConnection GetInstance()
        {
            try
            {

                if (conn == null)
                {
                    SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                    consStringBuilder.UserID = ReadSetting("Name");
                    consStringBuilder.Password = ReadSetting("Password");
                    consStringBuilder.InitialCatalog = ReadSetting("Database");
                    consStringBuilder.DataSource = ReadSetting("DataSource");
                    consStringBuilder.ConnectTimeout = 30;
                    conn = new SqlConnection(consStringBuilder.ConnectionString);
                    conn.Open();
                }
            } catch (SqlException e)
            {
                Console.WriteLine("Wasnt able to connect to database, try again later or call administrator ERR: {0}",e.Message);
            }
            return conn;
        }

        public static void CreateDatabase()
        {
            throw new NotImplementedException();
        }
        private static string ReadSetting(string key)
        {
            //nutno doinstalovat, VS nabídne doinstalaci samo
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
