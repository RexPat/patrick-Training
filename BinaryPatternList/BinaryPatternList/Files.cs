﻿using Newtonsoft.Json;
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

        private string path  = @"C:\Users\GRL\Downloads\128_ddr_sw_fpga_fw_reports";
        public string[] getFiles()
        {
            string curDirConfig = Directory.GetCurrentDirectory();
            string configPath = curDirConfig + @"\config.json";
            string jsonConfig = File.ReadAllText(configPath);
            JObject jObjectConfig = JObject.Parse(jsonConfig);
            string fileExtension = (string)jObjectConfig["FILE_EXTENEION"];
            string[] files = Directory.GetFiles(path, fileExtension, SearchOption.AllDirectories);

            inpFilesList = files.ToList();
            return files;
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

            
            var object1 = new { binaryFileCount = getFiles().Length, searchPattern = pattern };
            parentObject.Add("DATA", object1);
            // The path to the binary file

            

            string output = JsonConvert.SerializeObject(parentObject);
            string loc = @$"C:\Users\GRL\Desktop\output{count1}.json";
            File.WriteAllText(loc, output);

            return addressObjectList;




        }
    }
}