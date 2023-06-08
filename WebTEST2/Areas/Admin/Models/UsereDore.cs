using System;
using System.Collections.Generic;
using System.Text.Json;

namespace WebTEST2.Areas.Admin.Models
{
    public class UsereDore
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Reshte { get; set; }
        public List<VijegiModel> Vijegiha { get; set; }
        public List<int> VDid { get; set; }
        public List<string> Mohtava_Title { get; set; }
        public List<List<bool>> Mohtava_Choiced { get; set; }
        public void Deserializee(string choicess, int i)
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
