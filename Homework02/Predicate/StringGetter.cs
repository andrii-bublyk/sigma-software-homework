using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicate
{
    class StringGetter
    {
        public event Predicate<string> StringGenerated;

        public void Run()
        {
            while (true)
            {
                string str = Console.ReadLine();
                StringGenerated.Invoke(str);
            }
        }
    }
}
