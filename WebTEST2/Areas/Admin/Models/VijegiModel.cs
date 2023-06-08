
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace WebTEST2.Areas.Admin.Models
{
    public class VijegiModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public List<Choices> Choices { get; set; }
        public VijegiModel()
        {

        }
        public VijegiModel(string type, string title, string choicess)
        {
            this.Type = type;
            this.Title = title;
            this.Choices = JsonSerializer.Deserialize<List<Choices>>(choicess);
        }
        public string stringjson()
        {

            string _this = "ss";
            _this = JsonSerializer.Serialize(this.Choices);
            return _this;
        }

    }
    public class Choices
    {
        public string data { get; set; }
        public int number { get; set; }
    }
}
