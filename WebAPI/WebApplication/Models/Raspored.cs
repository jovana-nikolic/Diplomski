using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CRaspored
    {
        public int id { get; set; }

        public int student { get; set; }

        public int lab_vezba { get; set; }

        public DateTime? pocetak_laba { get; set; }

        public DateTime? zavrsetak_laba { get; set; }

        public string getDateTimeText(DateTime myDateTime)
        {
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return sqlFormattedDate;
        }
    }
}