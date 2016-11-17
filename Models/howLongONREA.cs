using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace Multilink2.Models
{
    public class howLongONREA
    {
        public int id { get; set; }
        public int office_id { get; set; }
        public DateTime rea_start { get; set; }
        public int days_on_rea { get; set; }
        public String address { get; set; }
        public String suburb { get; set; }
        public String postcode { get; set; }
        public String sales_method { get; set; }
        public String lister { get; set; }
        public String manager { get; set; }
        public int days_on_dca { get; set; }
        public DateTime dca_start { get; set; }

        public static List<howLongONREA> GetList(String SalesMethod, int _office_id)
        {
            List<howLongONREA> howLongONREAData = new List<howLongONREA>();
            DB db = new DB();
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                String _sales_method = "";
                query.CommandText = "exec howlongonrea  "+_office_id;
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {
                    _sales_method = reader.GetString(9);
                    _sales_method = _sales_method.Trim();

                    if (SalesMethod == "Rentals")
                    {
                        if (_sales_method != "Rent")
                            continue;
                    }
                    if (SalesMethod == "Sales")
                    {
                        if (_sales_method == "Rent")
                            continue;
                    }

                    howLongONREA newRecord = new howLongONREA();
                    newRecord.id = reader.GetInt32(0);
                    newRecord.office_id = reader.GetInt32(1);
                    newRecord.rea_start = reader.GetDateTime(2);
                    newRecord.days_on_rea = reader.GetInt32(3);
                    if (reader[4] != null)
                        newRecord.dca_start = reader.GetDateTime(4);
                    newRecord.days_on_dca = reader.GetInt32(5);
                    newRecord.address = reader.GetString(6);
                    newRecord.suburb = reader.GetString(7);
                    newRecord.postcode = reader.GetString(8);
                    newRecord.sales_method = reader.GetString(9);
                    newRecord.lister = reader.GetString(10);
                    newRecord.manager = reader.GetString(11);
                    howLongONREAData.Add(newRecord);
                }
            }
            db.close();
            return howLongONREAData;
        }

    }
}