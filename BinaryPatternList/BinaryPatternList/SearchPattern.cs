using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPatternList
{


    public class SearchPattern

    {
        public String value { get; set; }
        public int count { get; set; }
        public List<PatternAddress> address { get; set; }
        public JArray getSearchPatterArray()
        {
            string curDirConfig = Directory.GetCurrentDirectory();
            string filePath = curDirConfig + @"\data.json";
            string json = File.ReadAllText(filePath);
            JObject jObject = JObject.Parse(json);
            JArray patternArray = (JArray)jObject["SEARCH_PATTERN"];
            return patternArray;
            
        }

        public List<byte> getSearchPatternDecimalList(string pattern)
        {
            var intValues = new List<byte>();
            string[] hexValues = pattern.Split(' ');
            foreach (string ch in hexValues)
            {
                intValues.Add(Convert.ToByte(ch, 16));
            }
            return intValues;
        }

        


    }
}
