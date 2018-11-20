using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework02
{
    class AlphaNumbericCollector
    {
        private List<string> container = new List<string>();

        public void ProcessString(string str)
        {
            if (str.Any(char.IsDigit))
            {
                container.Add(str);
                Console.WriteLine("string added to AlphaNumbericCollector container");
            }
        }
    }
}
