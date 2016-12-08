using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Web;

namespace Multilink2.Models
{
    public class LoginModel { 

       [Required]
        [Display(Name = "User name")]
        public string UserName { get; set;  }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        bool? rememberMe;
        [Display(Name = "Remember me?")]
        public bool? RememberMe
        {
            get { return rememberMe ?? false; }
            set { rememberMe = value; }
        }

        public string login_result;
        public string full_name;
        public int user_office_id;
        public int user_id;
        public string user_photo;

        public Boolean login()
        {

            DB db = new DB();
            Boolean _result = false;
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                //exec wl_login_3 'support@multilink.com.au','jc','',''
                //FULL_NAME,USER_OFFICE_ID,USER_ID,USER_POSITION,USER_EMAIL,
                query.CommandText = "exec wl_login_3 '" + UserName + "', '" + Password+ "','',''";                
                OdbcDataReader reader = query.ExecuteReader();
                if (reader.Read() == true)
                {
                    _result = true;
                    if (reader.FieldCount < 2)
                    {
                        login_result = reader.GetString(0);
                        _result = false;
                    }                   
                    else
                    {
                        full_name = reader.GetString(0);
                        user_office_id = reader.GetInt32(1);
                        user_id = reader.GetInt32(2);
                        user_photo = "user_photo_" + user_id.ToString() + "_" + user_office_id.ToString()+".jpg";
                    }                    

                }
                reader.Close();
                if (_result == true)
                {
                    query.CommandText = String.Format("select [USER_PHOTO] from users where [USER_ID] = {0} and [USER_OFFICE_ID] = {1}", user_id, user_office_id);
                    reader = query.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        byte[] data = (byte[])reader[0]; //be careful here number 1 is 2nd column in DB (1st is picName, 2nd is Image)
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                        {
                            string path = HttpContext.Current.Server.MapPath("~/images/");
                            // <img src="~/images/user_photo.jpg" width="100" />
                           // user_photo = path + user_photo;
                            using (FileStream file = new FileStream(path + user_photo, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                file.Write(data, 0, (int)data.Length);
                            }
                            user_photo = "/images/" + user_photo;
                        }
                    }
                }                
            };
            db.close();
            return _result;
        }
    }
}