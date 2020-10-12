using Baza;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class StudentController : ApiController
    {
        public class Test
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Login([FromBody]Test obj)
        {
            var u = "test";
            var p = "test";
            if (u == obj.Username && p == obj.Password)
            {
                var ident = new ClaimsIdentity(
                  new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, obj.Username),
              new Claim(ClaimTypes.Name,obj.Username),
                  },
                  DefaultAuthenticationTypes.ApplicationCookie);

                //HttpContext.Current.GetOwinContext().Authentication.SignIn(
                //   new AuthenticationProperties { IsPersistent = true }, ident);

                var symmetricKey = Convert.FromBase64String("qwertyuioplkjhgfdsazxcvbnmqwertlkjfdslkjflksjfklsjfklsjdflskjflyuioplkjhgfdsazxcvbnmmnbv");
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = ident,

                    Expires = now.AddDays(7),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(symmetricKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var stoken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(stoken);


                return Ok(new { token }); // auth succeed 
            }
            // invalid username or password
            ModelState.AddModelError("", "invalid username or password");
            return Ok();
        }

        public IHttpActionResult Get()
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var list = db.STUDENTs.Select(studentDB => new Student
                {
                    ID = studentDB.ID,
                    ime = studentDB.ime,
                    indeks = studentDB.indeks,
                    prezime = studentDB.prezime
                }).ToList();
                return Json(list);
            }
        }

        public string Post(Baza.STUDENT s)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    db.STUDENTs.Add(s);
                    db.SaveChanges();
                }

                return "Added Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
        }


        public string Put(Student s)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var studentDB = db.STUDENTs.FirstOrDefault(x => x.ID == s.ID);

                    studentDB.ime = s.ime;
                    studentDB.prezime = s.prezime;
                    studentDB.indeks = s.indeks;

                    db.SaveChanges();
                }

                return "Updated Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Update!!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                using (BAZAEntities db = new BAZAEntities())
                {
                    var studentDB = db.STUDENTs.FirstOrDefault(x => x.ID == id);

                    db.STUDENTs.Remove(studentDB);

                    db.SaveChanges();
                }

                return "Deleted Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Delete!!";
            }
        }

        public IHttpActionResult GetStudent(int id)
        {
            using (BAZAEntities db = new BAZAEntities())
            {
                var studentDB = db.STUDENTs.FirstOrDefault(x => x.ID == id);
                if (studentDB == null)
                {
                    return BadRequest();
                }

                var s = new Student
                {
                    ID = studentDB.ID,
                    ime = studentDB.ime,
                    indeks = studentDB.indeks,
                    prezime = studentDB.prezime
                };

                return Json(s);
            }
        }

        //public Student SomeMethod(int id)
        //{
        //    var con = ConfigurationManager.ConnectionStrings["AppDB"].ToString();

        //    Student matchingPerson = new Student();
        //    using (SqlConnection myConnection = new SqlConnection(con))
        //    {
        //        string oString = "Select * from STUDENT where ID=@id";
        //        SqlCommand oCmd = new SqlCommand(oString, myConnection);
        //        oCmd.Parameters.AddWithValue("@ID", id);
        //        myConnection.Open();
        //        using (SqlDataReader oReader = oCmd.ExecuteReader())
        //        {
        //            while (oReader.Read())
        //            {
        //                matchingPerson.ime = oReader["ime"].ToString();

        //                matchingPerson.prezime = oReader["prezime"].ToString();
        //            }

        //            myConnection.Close();
        //        }
        //    }
        //    return matchingPerson;
        //}


    }
}

