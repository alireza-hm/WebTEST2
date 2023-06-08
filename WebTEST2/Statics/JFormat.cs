using Newtonsoft.Json;

namespace WebTEST2.Statics
{
    public class JFormat
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Mohtava { get; set; }
        public string[] Choices { get; set; }
        public int[] Choiced { get; set; }
        public JFormat(string jj)
        {
            this.Type = JsonConvert.DeserializeObject<JFormat>(jj).Type;
            this.Name = JsonConvert.DeserializeObject<JFormat>(jj).Name;
            this.Mohtava = JsonConvert.DeserializeObject<JFormat>(jj).Mohtava;
            this.Choices = JsonConvert.DeserializeObject<JFormat>(jj).Choices;
            this.Choiced = JsonConvert.DeserializeObject<JFormat>(jj).Choiced;
        }
        public string stringjson(JFormat jf)
        {
            string _this = "ss";
            _this = JsonConvert.SerializeObject(jf);
            return jf.ToString();
        }
    }
}
