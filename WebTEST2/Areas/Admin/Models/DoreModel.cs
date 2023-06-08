using System;
using System.Collections.Generic;

namespace WebTEST2.Areas.Admin.Models
{
    public class DoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date {get; set; }
        public int Long { get; set; }
        public string Image { get; set; }
        public string Descs { get; set; }
        public int AdminId { get; set; }
        public int CatId { get; set; }
        public List<VijegiModel> Vijegiha { get; set; }
        public List<bool> CheckVijegiha { get; set; }

    }
}
