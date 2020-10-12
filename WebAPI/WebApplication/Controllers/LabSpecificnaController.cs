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
    public class LabSpecificnaController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var list = db.LAB_SPECIFICNA.Select(lab_specificnaDB => new LabSpecificna
                {
                    ID = lab_specificnaDB.ID,
                    vremeOd = Convert.ToDateTime(lab_specificnaDB.vremeOd),
                    vremeDo =Convert.ToDateTime(lab_specificnaDB.vremeDo),
                    redosled=lab_specificnaDB.redosled,
                    Id_lab_vezbe=lab_specificnaDB.Id_Lab_vezbe

                }).ToList();
                return Json(list);
            }
            //string query = @"
            //        select vremeOd, vremeDo, redosled from
            //        dbo.LAB_SPECIFICNA
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

        public string Post(Baza.LAB_SPECIFICNA lv)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    db.LAB_SPECIFICNA.Add(lv);
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
            //        insert into dbo.LAB_SPECIFICNA values
            //        (" + lv.vremeOd + @", " + lv.vremeDo + @",'" + lv.redosled + @"'," + lv.Id_lab_vezbe + @")
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


        public string Put(LabSpecificna lv)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var lab_specificnaDB = db.LAB_SPECIFICNA.FirstOrDefault(x => x.ID == lv.ID);

                    lab_specificnaDB.vremeOd = lv.vremeOd;
                    lab_specificnaDB.vremeDo = lv.vremeDo;
                    lab_specificnaDB.redosled = lv.redosled;
                    lab_specificnaDB.Id_Lab_vezbe = lv.Id_lab_vezbe;

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
            //        update dbo.LAB_SPECIFICNA set vremeOd=
            //        " + lv.vremeOd + @", vremeDo=" + lv.vremeDo + @",redosled='" + lv.redosled + @"'
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
                    var lab_specificnaDB = db.LAB_SPECIFICNA.FirstOrDefault(x => x.ID == id);

                    db.LAB_SPECIFICNA.Remove(lab_specificnaDB);

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
            //        delete from dbo.LAB_SPECIFICNA 
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

        public IHttpActionResult GetLabS(int id)
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var lab_specificnaDB = db.LAB_SPECIFICNA.FirstOrDefault(x => x.ID == id);
                if (lab_specificnaDB == null) return BadRequest();

                var s = new LabSpecificna
                {
                    ID = lab_specificnaDB.ID,
                    vremeOd=Convert.ToDateTime( lab_specificnaDB.vremeOd),
                    vremeDo=Convert.ToDateTime( lab_specificnaDB.vremeDo),
                    redosled=lab_specificnaDB.redosled,
                    Id_lab_vezbe=lab_specificnaDB.Id_Lab_vezbe

                };

                return Json(s);
            }
            //string query = @"
            //        select vremeOd, vremeDo, redosled from
            //        dbo.LAB_SPECIFICNA where ID=" + id + @"
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
