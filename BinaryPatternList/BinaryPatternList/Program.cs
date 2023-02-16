// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BinaryPatternList;

namespace binaryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("Start time: " + start);
            

            SearchPattern searchPattern = new SearchPattern();
            JArray patternList = searchPattern.getSearchPatterArray();
            var patternDecimalList = new List<List<byte>>();
            var outputModelList = new List<OutputModel>();
            
            
            
            int count = 0;
            foreach(string pat in patternList)
            {
                patternDecimalList.Add(searchPattern.getSearchPatternDecimalList(pat));
            }
            
            Files getFiles = new Files();

            foreach(string file in getFiles.getFiles())
            {
                Console.WriteLine($"{file}");
                byte[] binaryData = File.ReadAllBytes(file);
                var address = new List<PatternAddress>();
                var patternInfoList = new List<SearchPattern>();
                foreach (string pat in patternList)
                {
                   address = getFiles.giveCountJson(binaryData, searchPattern.getSearchPatternDecimalList(pat), count, file);
                   count++;
                   patternInfoList.Add(new SearchPattern() { address = address, count = address.Count, value = pat});
                }

                
                outputModelList.Add(new OutputModel() { inputFilePath = file, patternInfo = patternInfoList});
                
            }




            //getOutput.outmod

            DateTime end = DateTime.Now;
            Console.WriteLine("End time: " + end);
            Console.WriteLine("Time taken: " + end.Subtract(start));
            Console.ReadKey();
        }
        
    }
}
        






