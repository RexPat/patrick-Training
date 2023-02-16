using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPatternList
{
    public class OutputModel
    {
        public string inputFilePath { get; set; }
        public int minData { get; set; } = 0;
        public int maxData { get; set; } = 0;
        public bool sd { get; set; } = false;

        public List<SearchPattern> patternInfo { get; set; }

        
    }


    
}
