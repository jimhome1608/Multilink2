using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace Multilink2.Models
{
    public class DB
    {

        public OdbcConnection odbcConnection;

        private int counter = 0;

        public String open(string Connection = "MultilinkConnection")
        {
            OdbcConnectionStringBuilder connectionString = new OdbcConnectionStringBuilder();
            connectionString.Driver = "SQL Server";
            connectionString["Server"] = "www.multilink.com.au";
            connectionString["Dsn"] = "ProplinkServer";
            connectionString["Uid"] = "sa";
            connectionString["Pwd"] = "j3ig974h30";
            //con = new OdbcConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString()); 
            odbcConnection = new OdbcConnection(connectionString.ConnectionString);
            try
            {
                bool b = true;
                counter++;
                if (odbcConnection.State.ToString() != "Open")
                {
                    odbcConnection.Open();
                    return odbcConnection.State.ToString() + " " + counter.ToString();
                }
                else
                {
                    return odbcConnection.State.ToString() + " " + counter.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String close()
        {
            try
            {
                odbcConnection.Close();
                return "closed";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                close();
            }
        }


    }
}