﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Data.Odbc;

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

        public Boolean login()
        {

            DB db = new DB();
            Boolean _result = false;
            db.open();
            using (OdbcCommand query = new OdbcCommand("", db.odbcConnection))
            {
                //exec wl_login_3 'support@multilink.com.au','jc','',''
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
                    }

                }
            };
            db.close();
            return _result;
        }
    }
}