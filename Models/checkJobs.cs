using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

/*
 
    exec checkjobs

1.01 RVW Export2	20161216	80000	1	D3D990EB-64EF-4655-87E3-9348DB620C06
1.1.1 RCA Export2	20161216	82000	1	7AE2456F-6E31-482F-B3AF-ED2BCF7D400B
1.2.1 DCA Export2	20161216	84000	1	11A3CDA4-A786-4FC8-BAD3-971ED5ABE2FD
Elite Real Estate	20161216	84500	1	484216BC-5C9E-4831-B2E3-D3A06A1A23E7
EXEC msdb.dbo.sp_start_job N'1.01 RVW Export2';	n/a	n/a	0	Dec 16 2016  8:53AM
UPDATE MSDB.dbo.sysjobs SET Enabled = 1 WHERE name = '1.01 RVW Export2';	n/a	n/a	0	Dec 16 2016  8:53AM

 */

namespace Multilink2.Models
{
    public class CheckJobs
    {
        public String JobNameReadable { get; set; } //1.1.1 RCA Export2
        public String JobName { get; set; }
        public String LastRunDate { get; set; }
        public String lastRunTime { get; set; }
        public String Enabled { get; set; }
        public String JobID { get; set; }
        public String StatusReport { get; set; }
        public String StatusReport2 { get; set; }
        public int NQR { get; set; }


        public static List<CheckJobs> GetList()
        {
            DateTime _LastRun;
            int _year;
            int _month;
            int _day;
            int _hour;
            int _minute;

            List<CheckJobs> checkJobsRecords = new List<CheckJobs>();
            DB db = new DB();
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                String _sales_method = "";
                query.CommandText = "exec CheckJobs  ";
                OdbcDataReader reader = query.ExecuteReader();
                while (reader.Read() == true)
                {

                    CheckJobs newRecord = new CheckJobs();
                    newRecord.JobNameReadable = "";
                    newRecord.JobName = reader["JobName"].ToString();
                    if (newRecord.JobName == "1.1.1 RCA Export2") newRecord.JobNameReadable = "REA - XML Upload to www.realestate.com.au";
                    if (newRecord.JobName == "1.2.1 DCA Export2") newRecord.JobNameReadable = "DCA - XML Upload to www.domain.com.au";
                    if (newRecord.JobName == "1.01 RVW Export2") newRecord.JobNameReadable = "RVW - XML Upload to www.realestateview.com.au";
                    if (newRecord.JobNameReadable == "")
                        continue;
                    newRecord.NQR = 0;
                    newRecord.LastRunDate = reader["LastRunDate"].ToString();
                    Int32.TryParse(newRecord.LastRunDate.Substring(0, 4), out _year);
                    Int32.TryParse(newRecord.LastRunDate.Substring(4, 2), out _month);
                    Int32.TryParse(newRecord.LastRunDate.Substring(6, 2), out _day);                    
                    newRecord.lastRunTime = reader["lastRunTime"].ToString();
                    Int32.TryParse(newRecord.lastRunTime.Substring(0, 2), out _hour);
                    Int32.TryParse(newRecord.lastRunTime.Substring(2, 2), out _minute);
                    _LastRun = new DateTime(_year, _month, _day, _hour, _minute,0);
                    TimeSpan duration = DateTime.Now - _LastRun;
                    double _LastRunMinutesAgo = duration.TotalMinutes;
                    newRecord.StatusReport = String.Format("Last run/upload: {0:0} minutes ago - "+ _LastRun.ToString(), _LastRunMinutesAgo);
                    if (_LastRunMinutesAgo < 70)
                        newRecord.StatusReport2 = "All correct.  Job is running as expected";
                    else
                    {
                        newRecord.NQR = 1;
                        newRecord.StatusReport2 = "Error/Warning.  This job is overdue and may have crashed or been disabled.";
                    }

                    newRecord.Enabled = reader["Enabled"].ToString();
                    newRecord.JobID = reader["JobID"].ToString();                    
                    checkJobsRecords.Add(newRecord);
                }
            }
            db.close();
            return checkJobsRecords;
        }
    }
}