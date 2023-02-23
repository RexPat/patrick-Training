using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPatternList
{
    public class Files
    {
        public List<OutputModel> outModList = new List<OutputModel>();
        List<string> inpFilesList = new List<string>();

        
        public List<string> getFiles()
        {
            List <string> files = new List<string> ();
            string path_ = @"";
            string curDirConfig = Directory.GetCurrentDirectory();
            string configPath = curDirConfig + @"\config.json";
            string jsonConfig = File.ReadAllText(configPath);
            JObject jObjectConfig = JObject.Parse(jsonConfig);
            string fileExtension = (string)jObjectConfig["FILE_EXTENEION"];
            JArray pathArray = (JArray)jObjectConfig["DIRECTORY_PATH"];
            
            foreach (string pt in pathArray)
            {
                path_ = path_ + "\\" + pt;
            }
            path_ = path_.Substring(1);
            files = Directory.GetFiles(path_, fileExtension, SearchOption.AllDirectories).ToList();
            
            
            
            return files;
        }

        public void createCSV(List <string> value)
        {
            // Path to the CSV file
            string filePath = @"D:\patternSearchAppOutput.csv";

            SearchPattern stobj= new SearchPattern();
               

            // Create the file if it doesn't exist
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            // Write some data to the CSV file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
                writer.WriteLine("filePath, pattern, count");
                foreach(string val in value)
                {
                    writer.WriteLine(val);
                }   
                
                
            }
        }

        public List<PatternAddress> giveCountJson(byte[] binaryData, List <byte> pattern, int count1, string file1)
        {
            //iterate file
            //instantiate - OutputModel, update the file path

            //>>>>>> iterate pattern
            //>>>>>> create PatternInfo 
            //>>>>>>>>>>>>>> find pattern logic
            //>>>>>>>>>>>>>> update PatternInfo 
            //>>>>>> add PatternInfo into OutputModel
            //add OutputModel into outModList

           
            var parentObject = new Dictionary<string, object>();
            int count = 0;
            




            var list1 = pattern;
            var binaryList = binaryData.ToList();
            int i = 0;
            var addressObjectList = new List<PatternAddress>();
            var patternIndexList = new List <long>();
            while(i <= binaryList.Count - pattern.Count)
            {
                if (binaryList.GetRange(i, pattern.Count).SequenceEqual(pattern))
                {
                    patternIndexList.Add(i);
                    i = i + pattern.Count - 1;
                }
                i++;
            }

            foreach(long k in patternIndexList)
            {
                addressObjectList.Add(new PatternAddress() { address = k });
            }



            var jsonObject = new { count = patternIndexList.Count, filePath = file1 };
            parentObject.Add(Convert.ToString(count), jsonObject);
            count++;

            
            var object1 = new { binaryFileCount = getFiles().Count, searchPattern = pattern };
            parentObject.Add("DATA", object1);
            
            // The path to the binary file
            
            

            string output = JsonConvert.SerializeObject(parentObject);
            //string loc = @$"C:\Users\GRL\Desktop\output{count1}.json";
            //File.WriteAllText(loc, output);
            
            return addressObjectList;




        }
    }
}
