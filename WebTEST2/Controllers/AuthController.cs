using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebTEST2.Models;

namespace WebTEST2.Controllers
{
    public class AuthController : Controller
    {
        SqlConnection _con = new SqlConnection(Statics.Cstring.Value);

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel Rg)
        {
            //check password
            if (Rg.Password != Rg.Repassword)
            {

            }

            //check empty user
            bool icheck = true;
            SqlCommand command = new SqlCommand("select Id from Users where Id = " + Rg.Id + ";", _con);
            _con.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (reader["Id"].ToString() != "")
                    {
                        icheck = false;
                    }
                }
            }
            _con.Close();
            //select reshte id
            string reshte = "";
            string cmd = "select Id from Reshteha where Name = '" + Rg.Reshte + "' ;";
            command = new SqlCommand(cmd, _con);
            _con.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    reshte = reader["Id"].ToString();
                }
            }
            _con.Close();
            //insert user
            _con.Open();
            string ss = "insert into Users values (@Id , @Name , @Family , @Fathername , @Code_meli , @Reshte , @Password , @Role , @Valid);";
            command = new SqlCommand(ss, _con);
            command.Parameters.AddWithValue("@Id", Rg.Id);
            command.Parameters.AddWithValue("@Name", Rg.Name);
            command.Parameters.AddWithValue("@Family", Rg.Family);
            command.Parameters.AddWithValue("@Fathername", Rg.Father_name);
            command.Parameters.AddWithValue("@Code_meli", Rg.Code_meli);
            command.Parameters.AddWithValue("@Reshte", reshte);
            command.Parameters.AddWithValue("@Password", Rg.Password);
            command.Parameters.AddWithValue("@Role", "User");
            command.Parameters.AddWithValue("@Valid", "False");
            command.ExecuteNonQuery();
            _con.Close();
            return Redirect("Login");
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Login( LoginModel Rg)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            string rolee = "";
            _con.Open();
            string ss = "select Password,Role from Users Where Id = @Id;";
            SqlCommand command = new SqlCommand(ss, _con);
            command.Parameters.AddWithValue("@Id", Rg.Id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (reader["Password"].ToString() != Rg.Password)
                    {
                        ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
                        return View();
                    }
                    rolee = reader["Role"].ToString();
                }
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Rg.Id.ToString()),
                new Claim(ClaimTypes.Role,rolee),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            if(rolee == "Admin")
                return Redirect("../Admin/Panel");
            return Redirect("../User/Panel");

        }
    }
}
