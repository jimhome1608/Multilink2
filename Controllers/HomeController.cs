using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Odbc;
using Multilink2.Models;

namespace Multilink2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new InternetEnquiresList();
            model = readInternetEnquires();
            return View("Index", model);
        }

        protected OdbcConnection odbcConnection;

        private int counter = 0;

        public String openODBCConnection(string Connection = "MultilinkConnection")
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

        public String CloseODBCConnection()
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

        private String abbreviateOfficeNames(String _officeName)
        {
            _officeName.Trim();
            _officeName = _officeName.Replace("Noel Jones", "N.Jones:");
            _officeName = _officeName.Replace("Barry Plant", "B.Plant:");
            _officeName = _officeName.Replace("Real Estate", " ");
            _officeName = _officeName.Replace("Management", " ");
            _officeName = _officeName.Replace("Pty Ltd", " ");
            _officeName = _officeName.Replace("Development", " ");
            _officeName = _officeName.Replace("Waverley City First National", "1st Nat.: Glen Waverley");
            return _officeName;

        }

        public InternetEnquiresList readInternetEnquires()
        {
            var model = new InternetEnquiresList();
            int _count = 0;
            model.count = 0;
            openODBCConnection();
            using (OdbcCommand query = new OdbcCommand("", odbcConnection))
            {
                query.CommandText = "select count(*) as web_enquiries, address from [webEnquiriesLog]  a join prop B on A.MLS_ID = B.ID AND A.MLS_OFFICE_ID = B.OFFICE_id group by address order by count(*) desc";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {

                    InternetEnquires newRecord = new InternetEnquires();
                    newRecord.address = reader["address"].ToString();
                    newRecord.count = reader["web_enquiries"].ToString();
                    Int32.TryParse(newRecord.count, out _count);

                    model.count = model.count + _count;
                    model.items.Add(newRecord);
                }
            }
            CloseODBCConnection();
            return model;
        }

        public AgentInfoList readAgentInfo(String _SearchString)
        {
            var model = new AgentInfoList();
            model.SearchString = _SearchString;
            openODBCConnection();
            using (OdbcCommand query = new OdbcCommand("", odbcConnection))
            {
                query.CommandText = "exec findPerson2 '" + _SearchString + "'";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {

                    AgentInfo newRecord = new AgentInfo();
                    newRecord.agent_name = reader["agent_name"].ToString();
                    newRecord.agent_name = abbreviateOfficeNames(newRecord.agent_name);
                    newRecord.agent_suburb = reader["agent_suburb"].ToString();
                    newRecord.agent_phone = reader["agent_phone"].ToString();
                    newRecord.full_name = reader["full_name"].ToString();
                    newRecord.user_mobile = reader["user_mobile"].ToString();
                    newRecord.user_phone_bh = reader["user_phone_bh"].ToString();
                    newRecord.user_email = reader["user_email"].ToString();
                    model.items.Add(newRecord);
                }
            }
            CloseODBCConnection();
            return model;
        }


        public OFI_RESULTS_AGENTS_List readOFI_RESULTS_AGENTS()
        {
            var model = new OFI_RESULTS_AGENTS_List();
            model.total_count = 0;
            openODBCConnection();
            using (OdbcCommand query = new OdbcCommand("", odbcConnection))
            {
                query.CommandText = "exec OFI_RESULTS_AGENTS";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {
                    OFI_RESULTS_AGENTS newRecord = new OFI_RESULTS_AGENTS();
                    newRecord.agent = reader["AGENT"].ToString();
                    newRecord.agent = abbreviateOfficeNames(newRecord.agent);
                    newRecord.agent_name = reader["AGENT_NAME"].ToString();
                    newRecord.agent_name = abbreviateOfficeNames(newRecord.agent_name);
                    newRecord.ofiActivity = reader["ofiActivity"].ToString();
                    model.total_count = model.total_count + Convert.ToInt32(reader["ofiActivity"]);
                    model.items.Add(newRecord);
                }
            }
            CloseODBCConnection();
            return model;
        }

        public List<WEBLINK_USER_STATS01> readWEBLINK_USER_STATS01()
        {
            var model = new List<WEBLINK_USER_STATS01>();
            openODBCConnection();
            using (OdbcCommand query = new OdbcCommand("", odbcConnection))
            {
                query.CommandText = "exec WEBLINK_USER_STATS01";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {
                    WEBLINK_USER_STATS01 newRecord = new WEBLINK_USER_STATS01();
                    newRecord.AGENT_NAME = reader["AGENT_NAME"].ToString();
                    newRecord.AGENT_NAME = abbreviateOfficeNames(newRecord.AGENT_NAME);

                    newRecord.FULL_NAME = reader["FULL_NAME"].ToString();
                    newRecord.USER_MOBILE = reader["USER_MOBILE"].ToString();
                    newRecord.FIRST_LOGIN = reader["FIRST_LOGIN"].ToString();
                    newRecord.LAST_LOGIN = reader["LAST_LOGIN"].ToString();
                    newRecord.HOW_MANY_DAYS_AGO = reader["HOW_MANY_DAYS_AGO"].ToString();
                    newRecord.HOW_MANY_LOGINS = reader["HOW_MANY_LOGINS"].ToString();
                    int _HOW_MANY_LOGINS = 0;
                    Int32.TryParse(newRecord.HOW_MANY_LOGINS, out _HOW_MANY_LOGINS);
                    if (_HOW_MANY_LOGINS > 10)
                        newRecord.HOW_MANY_LOGINS = newRecord.HOW_MANY_LOGINS + "<br /><i class=\"fa fa-thumbs-o-up fa-fw\"></i>";
                    model.Add(newRecord);                                        //<i class=" fa fa-thumbs-o-up fa-fw "></i>
                }
            }
            CloseODBCConnection();
            return model;
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
                CloseODBCConnection();
            }
        }


    }
}