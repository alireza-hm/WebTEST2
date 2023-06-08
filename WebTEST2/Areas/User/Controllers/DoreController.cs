using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTEST2.Areas.User.Models;

namespace WebTEST2.Areas.User.Controllers
{
    public class DoreController : UserControllerBase
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
            string cms = @"Select Doreha.Id,Doreha.Name,Doreha.Date,Doreha.Long,Doreha.Image,Doreha.Descs,Doreha.AdminId,Doreha.CatId,A.Valid
FROM Doreha
LEFT JOIN (SELECT Doreeuser.Valid,Doreeuser.Did From Doreeuser Where Doreeuser.Uid = " + HttpContext.User.Claims.First().Value + @") As A
ON Doreha.Id = A.Did;";
            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DoreModel model = new DoreModel()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Date = DateTime.Parse(reader["Date"].ToString()),
                            Long = int.Parse(reader["Long"].ToString()),
                            Image = reader["Image"].ToString(),
                            Descs = reader["Descs"].ToString(),
                            AdminId = int.Parse(reader["AdminId"].ToString()),
                            CatId = int.Parse(reader["CatId"].ToString()),
                            Valid = reader["Valid"].ToString(),
                        };
                        Dorehaha.Add(model);
                    }
                }
                _con.Close();
            }
            return View(Dorehaha.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Register(int id)
        {
            List<VijegiModel> Vijegihaa = new List<VijegiModel>();
            List<int> VDid = new List<int>();
            List<List<bool>> listtt = new List<List<bool>>();
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
                        if (item.Choices != null)
                            foreach (var ii in item.Choices)
                                listt.Add(false);
                        if (item.Type != "multi_choice")
                        {
                            listt.Add(false);
                            Choices s = new Choices() { data = "s", number = 1 };
                            List<Choices> ch = new List<Choices>();
                            ch.Add(s);
                            item.Choices = ch;
                        }

                        Vijegihaa.Add(item);
                        VDid.Add(reader.GetInt32(3));
                        listtt.Add(listt);
                    }
                }
                _con.Close();
            }

            MohtavaModel dore = new MohtavaModel() { DoreId = id, Vijegiha = Vijegihaa, VDid = VDid, Mohtava_Choiced = listtt };
            cms = "select Name,Date,Long,Image,Descs,AdminId,CatId from Doreha where Id = " + id + ";";
            using (SqlCommand cmd = new SqlCommand(cms, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dore.Name = reader["Name"].ToString();
                        dore.Date = DateTime.Parse(reader["Date"].ToString());
                        dore.Long = int.Parse(reader["Long"].ToString());
                        dore.Image = reader["Image"].ToString();
                        dore.Descs = reader["Descs"].ToString();
                        dore.AdminId = int.Parse(reader["AdminId"].ToString());
                        dore.CatId = int.Parse(reader["CatId"].ToString());
                    }
                }
                _con.Close();
                List<string> tlist = new List<string>();
                foreach (var ii in dore.VDid)
                    tlist.Add("s");
                dore.Mohtava_Title = tlist;
            }

            return View(dore);

        }
        [HttpPost]
        public IActionResult Register(MohtavaModel b)
        {
            _con.Open();
            string ss = "insert into Doreeuser (Uid , Did , Valid)  output inserted.Id values (@Uid , @Did , @Valid);";
            SqlCommand command = new SqlCommand(ss, _con);
            command.Parameters.AddWithValue("@Uid", HttpContext.User.Claims.First().Value);
            command.Parameters.AddWithValue("@Did", b.DoreId);
            command.Parameters.AddWithValue("@Valid", "true");
            int idd = (int)command.ExecuteScalar();
            _con.Close();
            /////////////
            for (int i = 0; i < b.Vijegiha.Count; i++)
            {
                var item = b.Vijegiha[i];
                _con.Open();
                ss = "insert into Mohtava values (@VDid , @UDid , @Mohtava);";
                command = new SqlCommand(ss, _con);
                command.Parameters.AddWithValue("@UDid", idd);
                command.Parameters.AddWithValue("@VDid", b.VDid[i]);
                string sss = "";
                if (item.Type == "multi_choice")
                    sss = b.Serializee(i);
                else
                    sss = b.Mohtava_Title[i];
                command.Parameters.AddWithValue("@Mohtava", sss);

                command.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToAction("List");
        }
    }
}
