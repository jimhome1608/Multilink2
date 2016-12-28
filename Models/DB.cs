using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using System.Configuration;

namespace Multilink2.Models
{
    public class DB
    {

        public OdbcConnection odbcConnection;

        private int counter = 0;

        public String open(string Connection = "MultilinkConnection")
        {
            String _connection = ConfigurationManager.ConnectionStrings["ProplinkServer"].ConnectionString;
            odbcConnection = new OdbcConnection(_connection);
            try
            {               
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
                odbcConnection.Dispose();
                return "closed";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}