using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.classes
{
    class ReadFromFile
    {
        public string text = "";
        public ReadFromFile(int dayNumber, int yearNumber)
        {
            // The files used in this example are created in the topic
            // How to: Write to a Text File. You can change the path and
            // file name to substitute text files of your own.

            // Example #1
            // Read the file as one string.
           // string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../input\Day" + dayNumber+".txt");
            text = System.IO.File.ReadAllText(@"../../../input/" + yearNumber + "/Day" + dayNumber + ".txt");


        }

        public string getTextValue()
        {
            return text;
        }


    }
}
