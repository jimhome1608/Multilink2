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

        public bool writeToDB()
        {
            if (items.Count() < 1)
                return true;
            DB db = new DB();
            db.open();
            OdbcCommand query = new OdbcCommand("", db.odbcConnection);
            OdbcDataReader reader;
            foreach (Inspections i in items)
            {
                String _sql = String.Format("exec wl_write_ofi_results3 {0}, {1}, {2}, '{3}', {4}, '{5}', '{6}', '{7}', {8}, {9}, {10}, '{11}', '{12}', '{13}', '{14}', {15}, '{16}',' {17}', {18}, '{19}' ",
                            i.OFFICE_ID, i.ID, i.USER_ID, i.OFI_DATE, i.OFI_IDX, i.NAME, i.PHONE, i.MY_NOTESPOTENTIAL_SELLER, i.INTERESTED, 0, 0, i.PRICE, i.EMAIL, "", i.SURNAME, i.WANTS_SECT32,
                            i.RESULT, i.NOTES, 0, "Inspected");
                query.CommandText = _sql;

                reader = query.ExecuteReader();
                if (reader.Read() == true)
                {
                    i.OFI_IDX = reader.GetString(0);
                }
                reader.Close();
            }
            db.close();
            return true;
        }

        public List<Inspections> loadFromDB()
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


    /*
     The paramaters for writeData()
         wl_write_ofi_results3]
             @_OFFICE_ID int, @_ID int,@_USER_ID int, @_OFI_DATE varchar(50), @_OFI_IDX int,
             @_NAME varchar(255), @_PHONE varchar(255), @_VENDORNOTES varchar(4096),
             @_INTERESTED int,@_INVESTOR int, @_HOT_PROSPECT int,  @_PRICE  varchar(255),
             @_EMAIL varchar(255),  @_ADDRESS varchar(255),  @_SURNAME varchar(255), @_WANTS_SECT32 int, @_RESULT  varchar(255),
             @_BUYERNOTES varchar(4096),  @_POTENTIAL_SELLER int, @_ACTIVITY_TYPE varchar(255) 
    */

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