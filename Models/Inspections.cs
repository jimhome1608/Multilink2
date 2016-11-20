using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace Multilink2.Models
{
    //exec wl_get_localstorage 1, 86,283527, '10112016'

    public class InspectionList
    {
        public List<Inspections> items { get; set; }

        public InspectionList()
        {
            this.items = new List<Inspections>();
        }


        public List<Inspections> getData()
        {
            this.items.Clear();
            DB db = new DB();
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                query.CommandText = "exec wl_get_localstorage 1, 86,283527, '10112016'  ";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {
                    Inspections newRecord = new Inspections();
                    newRecord.OFFICE_ID = reader["OFFICE_ID"].ToString();
                    newRecord.ID = reader["ID"].ToString();
                    newRecord.USER_ID = reader["USER_ID"].ToString();
                    newRecord.OFI_DATE = reader["OFI_DATE"].ToString();
                    newRecord.OFI_IDX = reader["OFI_IDX"].ToString();
                    newRecord.PHONE = reader["PHONE"].ToString();
                    newRecord.NAME = reader["NAME"].ToString();
                    newRecord.SURNAME = reader["SURNAME"].ToString();
                    newRecord.PRICE = reader["PRICE"].ToString();
                    newRecord.NOTES = reader["NOTES"].ToString();
                    newRecord.RESULT = reader["RESULT"].ToString();
                    newRecord.EMAIL = reader["EMAIL"].ToString();
                    // newRecord.MY_NOTESPOTENTIAL_SELLER = reader["MY_NOTESPOTENTIAL_SELLER"].ToString();
                    newRecord.WANTS_SECT32 = reader["WANTS_SECT32"].ToString();
                    newRecord.INTERESTED = reader["INTERESTED"].ToString();
                    newRecord.MAYBE_INTERESTED = reader["MAYBE_INTERESTED"].ToString();
                    newRecord.NOT_INTERESTED = reader["NOT_INTERESTED"].ToString();
                    newRecord.ACTIVITY_TYPE = reader["ACTIVITY_TYPE"].ToString();
                    items.Add(newRecord);
                }
            }
            db.close();
            return items;
        }

    }

    public class Inspections
    {
        public String OFFICE_ID { get; set; }
        public String ID { get; set; }
        public String USER_ID { get; set; }
        public String OFI_DATE { get; set; }
        public String OFI_IDX { get; set; }
        public String PHONE { get; set; }
        public String NAME { get; set; }
        public String SURNAME { get; set; }
        public String PRICE { get; set; }
        public String NOTES { get; set; }
        public String RESULT { get; set; }
        public String EMAIL { get; set; }
        public String MY_NOTESPOTENTIAL_SELLER { get; set; }
        public String WANTS_SECT32 { get; set; }
        public String INTERESTED { get; set; }
        public String MAYBE_INTERESTED { get; set; }
        public String NOT_INTERESTED { get; set; }
        public String ACTIVITY_TYPE { get; set; }
    }
}