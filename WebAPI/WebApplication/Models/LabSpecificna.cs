using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LabSpecificna
    {
        public int ID { get; set; }

        public DateTime vremeOd { get; set; }

        public DateTime vremeDo { get; set; }

        public int? redosled { get; set; }

        public int? Id_lab_vezbe { get; set; }
    }
}