using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;
using Baza;

namespace WebApplication.Controllers
{
    public class LabVezbaController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var list = db.LAB_VEZBA.Select(lab_vezbaDB => new LabVezba
                {
                    ID = lab_vezbaDB.ID,
                    tag = lab_vezbaDB.tag,
                    naziv = lab_vezbaDB.naziv
                }).ToList();
                return Json(list);
            }
        
        }

        public string Post(Baza.LAB_VEZBA lv)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    db.LAB_VEZBA.Add(lv);
                    db.SaveChanges();
                }

                return "Added Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
            //try
            //{
            //    string query = @"
            //        insert into dbo.LAB_VEZBA values
            //        (" + lv.tag + @"', '" + lv.naziv + @"')
            //        ";

            //    DataTable table = new DataTable();
            //    using (var con = new SqlConnection(ConfigurationManager.
            //        ConnectionStrings["AppDB"].ConnectionString))
            //    using (var cmd = new SqlCommand(query, con))
            //    using (var da = new SqlDataAdapter(cmd))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        da.Fill(table);
            //    }

            //    return "Added Successfully!!";
            //}
            //catch (Exception)
            //{

            //    return "Failed to Add!!";
            //}
        }


        public string Put(LabVezba lv)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var lab_vezbaDB = db.LAB_VEZBA.FirstOrDefault(x => x.ID == lv.ID);

                    lab_vezbaDB.tag = lv.tag;
                    lab_vezbaDB.naziv = lv.naziv;                    

                    db.SaveChanges();
                }

                return "Updated Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Update!!";
            }
            //try
            //{
            //    string query = @"
            //        update dbo.LAB_VEZBA set tag=
            //        '" + lv.tag + @"', naziv='" + lv.naziv + @"'
            //        where ID=" + lv.ID + @"
            //        ";

            //    DataTable table = new DataTable();
            //    using (var con = new SqlConnection(ConfigurationManager.
            //        ConnectionStrings["AppDB"].ConnectionString))
            //    using (var cmd = new SqlCommand(query, con))
            //    using (var da = new SqlDataAdapter(cmd))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        da.Fill(table);
            //    }

            //    return "Updated Successfully!!";
            //}
            //catch (Exception)
            //{

            //    return "Failed to Update!!";
            //}
        }


        public string Delete(int id)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var lab_vezbaDB = db.LAB_VEZBA.FirstOrDefault(x => x.ID == id);

                    db.LAB_VEZBA.Remove(lab_vezbaDB);

                    db.SaveChanges();
                }

                return "Deleted Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Delete!!";
            }
            //try
            //{
            //    string query = @"
            //        delete from dbo.LAB_VEZBA 
            //        where ID=" + indeks + @"
            //        ";

            //    DataTable table = new DataTable();
            //    using (var con = new SqlConnection(ConfigurationManager.
            //        ConnectionStrings["AppDB"].ConnectionString))
            //    using (var cmd = new SqlCommand(query, con))
            //    using (var da = new SqlDataAdapter(cmd))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        da.Fill(table);
            //    }

            //    return "Deleted Successfully!!";
            //}
            //catch (Exception)
            //{

            //    return "Failed to Delete!!";
            //}
        }

        public IHttpActionResult GetLabVezba(int id)
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var lab_vezbaDB = db.LAB_VEZBA.FirstOrDefault(x => x.ID == id);
                if (lab_vezbaDB == null) return BadRequest();

                var s = new LabVezba
                {
                    ID = lab_vezbaDB.ID,
                    tag = lab_vezbaDB.tag,
                    naziv = lab_vezbaDB.naziv
                   
                };

                return Json(s);
            }
            //string query = @"
            //        select tag, naziv from
            //        dbo.LAB_VEZBA where ID=" + id + @"
            //        ";
            //DataTable table = new DataTable();
            //using (var con = new SqlConnection(ConfigurationManager.
            //    ConnectionStrings["AppDB"].ConnectionString))
            //using (var cmd = new SqlCommand(query, con))
            //using (var da = new SqlDataAdapter(cmd))
            //{
            //    cmd.CommandType = CommandType.Text;
            //    da.Fill(table);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, table);

        }
    }
}
