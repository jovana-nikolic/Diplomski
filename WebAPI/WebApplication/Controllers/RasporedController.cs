using Baza;
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

namespace WebApplication.Controllers
{
    [RoutePrefix("api/raspored")]
    public class RasporedController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select * from
                    dbo.RASPORED
                    inner join dbo.STUDENT 
                    on dbo.RASPORED.student = dbo.STUDENT.ID
                    inner join dbo.LAB_SPECIFICNA 
                    on dbo.RASPORED.lab_vezba = dbo.LAB_SPECIFICNA.ID
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["AppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }

        [HttpGet]
        [Route("prosek")]
        public HttpResponseMessage VR_prosek()
        {
            string query = "USP_PROSEK";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }
        public string Post(Baza.RASPORED rs)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    db.RASPOREDs.Add(rs);
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
            //        insert into dbo.Raspored values
            //        (" + rs.student + @", " + rs.lab_vezba + @", " + rs.pocetak_laba + @")
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

        //ovo mora bolje da se u uslovu stavi i student i lab vezba :D
        public string Put(CRaspored r)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var rasporedDB = db.RASPOREDs.FirstOrDefault(x => x.id == r.id);

                    rasporedDB.student = r.student;
                    rasporedDB.lab_vezba = r.lab_vezba;
                    rasporedDB.pocetak_laba = Convert.ToDateTime(r.pocetak_laba);
                    rasporedDB.zavrsetak_laba = Convert.ToDateTime(r.zavrsetak_laba);

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
            //        update dbo.Raspored set student=
            //        '" + r.student + @"', pocetak_laba=" + r.pocetak_laba + @"
            //        where lab_vezba=" + r.lab_vezba + @"
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


        public string Delete(int rs)
        {
            try
            {
                string query = @"
                    delete from dbo.RASPORED 
                    where lab_vezba=" + rs + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["AppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Delete!!";
            }
        }

        //public List<int> Get_id_Lab(string tag)
        //{
        //    var con = ConfigurationManager.ConnectionStrings["AppDB"].ToString();

        //    List<int> lista = new List<int>();
        //    using (SqlConnection myConnection = new SqlConnection(con))
        //    {
        //        string oString = "Select ID from LAB_VEZBA where tag=@tag";
        //        SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //        oCmd.Parameters.AddWithValue("@tag", tag);
        //        myConnection.Open();
        //        using (SqlDataReader oReader = oCmd.ExecuteReader())
        //        {
        //            while (oReader.Read())
        //            {
        //                lista.Add(Convert.ToInt32(oReader["ID"]));
        //            }

        //            myConnection.Close();
        //        }
        //    }
        //    return lista;
        //}
        //public List<List<double>> Get_statistike(List<int> lista)
        //{
        //    var con = ConfigurationManager.ConnectionStrings["AppDB"].ToString();

        //    List<List<double>> listaVracanje = new List<List<double>>();
        //    double dt;
            
        //    using (SqlConnection myConnection = new SqlConnection(con))
        //    {
        //        foreach (int li in lista)
        //        {
        //            string oString = "Select vremeOd, vremeDo from LAB_SPECIFICNA where Id_Lab_vezbe="+li;
        //            SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //            oCmd.Parameters.AddWithValue("@Id_Lab_vezbe", li);
        //            myConnection.Open();
        //            using (SqlDataReader oReader = oCmd.ExecuteReader())
        //            {
        //                while (oReader.Read())
        //                {
        //                    dt =Convert.ToDateTime(oReader["vremeDo"]).Subtract(Convert.ToDateTime(oReader["vremeOd"])).TotalMinutes;
        //                    listaVracanje[Convert.ToInt32(oReader["redosled"])].Add(dt);
        //                }

        //                myConnection.Close();
        //            }
        //        }
                
        //    }
        //    return listaVracanje;
        //}

    }
}
