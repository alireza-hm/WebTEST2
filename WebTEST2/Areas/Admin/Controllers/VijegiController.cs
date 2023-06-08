using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using WebTEST2.Areas.Admin.Models;

namespace WebTEST2.Areas.Admin.Controllers
{
    public class VijegiController : AdminControllerBase
    {
        SqlConnection _con = new SqlConnection(Statics.Cstring.Value);
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            List<VijegiModel> Vijegiha = new List<VijegiModel>();
            string cms = "select Type,Title,Choices,Id from Vijegiha ORDER BY Id";

            using (SqlCommand cmd = new SqlCommand(cms,_con))
                {
                    _con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        VijegiModel item = new VijegiModel(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        item.Id = reader.GetInt32(3);
                        Vijegiha.Add(item);
                        }
                    }
                    _con.Close();
                }
            return View(Vijegiha.AsEnumerable());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( VijegiModel b)
        {
            _con.Open();
            string ss = "insert into Vijegiha (Type,Title,Choices) VALUES (@type,@title,@choices); ";
            SqlCommand command = new SqlCommand(ss, _con);
            command.Parameters.AddWithValue("@type", b.Type);
            command.Parameters.AddWithValue("@title", b.Title);
            command.Parameters.AddWithValue("@choices", b.stringjson());
            command.ExecuteNonQuery();
            _con.Close();
            return RedirectToAction("List");
        }
    }
}
