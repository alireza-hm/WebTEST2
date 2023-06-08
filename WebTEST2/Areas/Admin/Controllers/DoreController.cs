using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTEST2.Areas.Admin.Models;

namespace WebTEST2.Areas.Admin.Controllers
{
    public class DoreController : AdminControllerBase
    {
        SqlConnection _con = new SqlConnection(Statics.Cstring.Value);

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult List()
        {
            List<DoreModel> Dorehaha = new List<DoreModel>();
            string cms = "select Name,Date,Long,Image,Descs,AdminId,CatId from Doreha ORDER BY Id";
            
            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DoreModel model = new DoreModel()
                        {
                            Name = reader["Name"].ToString(),
                            Date = DateTime.Parse(reader["Date"].ToString()),
                            Long = int.Parse(reader["Long"].ToString()),
                            Image = reader["Image"].ToString(),
                            Descs = reader["Descs"].ToString(),
                            AdminId = int.Parse(reader["AdminId"].ToString()),
                            CatId = int.Parse(reader["CatId"].ToString())
                        };
                        Dorehaha.Add(model);
                    }
                }
                _con.Close();
            }
            return View(Dorehaha.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<VijegiModel> Vijegihaa = new List<VijegiModel>();
            List<bool> checkk = new List<bool>();
            string cms = "select Type,Title,Choices,Id from Vijegiha ORDER BY Id";

            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VijegiModel item = new VijegiModel(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        item.Id = reader.GetInt32(3);
                        Vijegihaa.Add(item);
                        checkk.Add(false);
                    }
                }
                _con.Close();
            }
            DoreModel dore = new DoreModel() { Vijegiha = Vijegihaa, CheckVijegiha = checkk };
            return View(dore);
        }
        [HttpPost]
        public IActionResult Create(DoreModel b)
        {
            //
            int idd = 1;
            string ss = "insert into Doreha (Name,Date,Long,Image,Descs,AdminId,CatId) output inserted.Id values (@Name,@Date,@Long,@Image,@Descs,@AdminId,@CatId); ";
            SqlCommand command = new SqlCommand(ss, _con);
            _con.Open();
            command.Parameters.AddWithValue("@Name", b.Name);
            command.Parameters.AddWithValue("@Date", b.Date);
            command.Parameters.AddWithValue("@Long", b.Long);
            command.Parameters.AddWithValue("@Image", b.Image);
            command.Parameters.AddWithValue("@Descs", b.Descs);
            command.Parameters.AddWithValue("@AdminId", b.AdminId);
            command.Parameters.AddWithValue("@CatId", b.CatId);
            idd = (int)command.ExecuteScalar(); 
            _con.Close();
            //
            for (int i = 1; i <= b.CheckVijegiha.Count; i++)
            {
                _con.Open();
                if (b.CheckVijegiha[i-1])
                {
                    ss = "insert into Vijegiedoreha VALUES (@Did,@Vid); ";
                    command = new SqlCommand(ss, _con);
                    command.Parameters.AddWithValue("@Did", idd);
                    command.Parameters.AddWithValue("@Vid", b.Vijegiha[i-1].Id);
                    command.ExecuteNonQuery();
                }
                _con.Close();
            }
            return RedirectToAction("List");
        }
        public IActionResult UsersCourse(int id)
        {
            List<UsereDore> list = new List<UsereDore>();

            List<VijegiModel> Vijegihaa = new List<VijegiModel>();
            List<int> VDid = new List<int>();
            string cms = @"Select Vijegiha.Type,Vijegiha.Title,Vijegiha.Choices,A.Id
FROM Vijegiha
INNER JOIN(SELECT Vijegiedoreha.Vid, Vijegiedoreha.Id From Vijegiedoreha Where Vijegiedoreha.Did = " + id + @") As A
ON Vijegiha.Id = A.Vid; ";
            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<bool> listt = new List<bool>();
                        VijegiModel item = new VijegiModel(reader.GetString(0), reader.GetString(1), reader.GetString(2));

                        Vijegihaa.Add(item);
                        VDid.Add(reader.GetInt32(3));
                    }
                }
                _con.Close();
            }
            /////
            cms = @"Select Vijegiha.Type,Vijegiha.Title,Vijegiha.Choices,A.Id
FROM Vijegiha
INNER JOIN(SELECT Doreeuser.Vid, Vijegiedoreha.Id From Vijegiedoreha Where Vijegiedoreha.Did = " + id + @") As A
ON Vijegiha.Id = A.Vid; ";
            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<bool> listt = new List<bool>();
                        VijegiModel item = new VijegiModel(reader.GetString(0), reader.GetString(1), reader.GetString(2));

                        Vijegihaa.Add(item);
                        VDid.Add(reader.GetInt32(3));
                    }
                }
                _con.Close();
            }
            /////
            UsereDore dore = new UsereDore() { Vijegiha = Vijegihaa, VDid = VDid };

            return View(list);
        }
    }
}
