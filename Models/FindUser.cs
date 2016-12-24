using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multilink2.Models
{


    public class AgentInfoList
    {
        public string SearchString { get; set; }

        public List<AgentInfo> items { get; set; }

        public AgentInfoList()
        {
            this.items = new List<AgentInfo>();
        }

    }


    public class AgentInfo
    {

        public string agent_name { get; set; }
        public string agent_suburb { get; set; }
        public string agent_phone { get; set; }
        public string full_name { get; set; }
        public string user_mobile { get; set; }
        public string user_phone_bh { get; set; }
        public string user_email { get; set; }
        public string ya_password { get; set; }

    }
}