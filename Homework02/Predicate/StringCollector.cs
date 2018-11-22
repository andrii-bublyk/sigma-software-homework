using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicate
{
    class StringCollector
    {
        private List<string> container = new List<string>();

        public bool ProcessString(string str)
        {
            if (!str.Any(char.IsDigit))
            {
                container.Add(str);
                Console.WriteLine("string added to StringCollector container");

                return true;
            }

            return false;
        }
    }
}
