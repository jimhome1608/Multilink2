using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multilink2.Models;
using System.Data.Odbc;

namespace Multilink2.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/

        public ActionResult vwCheckJobs()
        {
            List<CheckJobs> _list;
            _list = CheckJobs.GetList();
            ViewBag.UserLocation = "Check XML Jobs";
            ViewBag.BaseLocation = "Multilink Support";
            return View(_list);
        }

        public ActionResult FindUser()
        {
            ViewBag.UserLocation = "Find User";
            ViewBag.BaseLocation = "Multilink Support";
            var model = new AgentInfoList();
            model = readAgentInfo("");
            return View("vwFindUser", model);
        }


        [HttpPost]
        public ActionResult RefreshAgent(AgentInfoList model)
        {
            ViewBag.UserLocation = "Find User";
            ViewBag.BaseLocation = "Multilink Support";
            model = readAgentInfo(model.SearchString);
            return View("vwFindUser", model);
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

        public AgentInfoList readAgentInfo(String _SearchString)
        {
            var model = new AgentInfoList();
            model.SearchString = _SearchString;
            DB db = new DB();
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
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
                    newRecord.ya_password = reader["ya_password"].ToString();
                    model.items.Add(newRecord);
                }
            }
            db.close();
            return model;
        }


    }
}
