using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string ime { get; set; }

        public string prezime { get; set; }

        public int? indeks { get; set; }
    }
}