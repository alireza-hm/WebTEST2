using System;
using System.Collections.Generic;
using System.Text.Json;

namespace WebTEST2.Areas.User.Models
{
    public class MohtavaModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Long { get; set; }
        public string Image { get; set; }
        public string Descs { get; set; }
        public int AdminId { get; set; }
        public int CatId { get; set; }
        public int DoreId { get; set; }
        public List<VijegiModel> Vijegiha { get; set; }
        public List<int> VDid { get; set; }
        public List<string> Mohtava_Title { get; set; }
        public List<List<bool>> Mohtava_Choiced { get; set; }
        public void Deserializee (string choicess,int i)
        {
            this.Mohtava_Choiced[i] = JsonSerializer.Deserialize<List<bool>>(choicess);
        }
        public string Serializee(int i)
        {
            string _this = "ss";
            _this = JsonSerializer.Serialize(this.Mohtava_Choiced[i]);
            return _this;
        }
    }
}
