﻿using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class HomeModel
    {
        public string LerNomeUsuario()
        {
            DAL objDal = new DAL();
            DataTable dt = objDal.RetDataTable("select * from usuario");
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["nome"].ToString();
                }

            return string.Empty;
        }
    }
}
