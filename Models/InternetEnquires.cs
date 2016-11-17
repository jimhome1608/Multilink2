using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace Multilink2.Models
{

    
    public class WebEnquires
    {
        const int DataItemsCount = 100;

        public String address { get; set; }
        public int total_count { get; set; }
        public int rea_count { get; set; }
        public int dca_count { get; set; }
        public int rvw_count { get; set; }
        public int homely_count { get; set; }
        public String manager { get; set; }
        public String sales_method { get; set; }
        public int days_on_web  { get; set; }
        public DateTime rea_start { get; set; }
        public DateTime dca_start { get; set; }
        public DateTime rvw_start { get; set; }
        public String enquiries_per_day { get; set; }
        public int inspections { get; set; }

        public static List<WebEnquires> GetList(int Office_id, String SalesMethod)
        {
            List<WebEnquires> typedList = new List<WebEnquires>();
            DB db = new DB();
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                int _count = 0;
                SalesMethod = SalesMethod.Replace(" ", "");
                query.CommandText = "exec get_webEnquiriesLog " + Office_id.ToString() + ", '"+ SalesMethod + "'";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {

                    WebEnquires newRecord = new WebEnquires();
                    newRecord.address = reader["PROP_ADDRESS"].ToString();
                    Int32.TryParse(reader["TOTAL_COUNT"].ToString(), out _count);
                    newRecord.total_count = _count;
                    Int32.TryParse(reader["REA_COUNT"].ToString(), out _count);
                    newRecord.rea_count = _count;
                    Int32.TryParse(reader["RVW_COUNT"].ToString(), out _count);
                    newRecord.rvw_count = _count;
                    Int32.TryParse(reader["DCA_COUNT"].ToString(), out _count);
                    newRecord.dca_count = _count;
                    Int32.TryParse(reader["HOMELY_COUNT"].ToString(), out _count);
                    newRecord.homely_count = _count;                    
                    newRecord.manager = reader["MANAGER"].ToString();
                    newRecord.sales_method = reader["SALES_METHOD"].ToString();
                    Int32.TryParse(reader["DAYS_ON_WEB"].ToString(), out _count);
                    newRecord.days_on_web = _count;
                    Int32.TryParse(reader["INSPECTIONS"].ToString(), out _count);
                    newRecord.inspections = _count;
                    float _enquiries_per_day = 0;
                    if (newRecord.days_on_web > 0)
                        _enquiries_per_day = (float) newRecord.total_count / (float) newRecord.days_on_web;
                    else
                        _enquiries_per_day = newRecord.total_count;
                    newRecord.enquiries_per_day = String.Format("{0:00.00}", _enquiries_per_day);
                    typedList.Add(newRecord);
                }
            }
            db.close();
            return typedList;
        }

    }

}