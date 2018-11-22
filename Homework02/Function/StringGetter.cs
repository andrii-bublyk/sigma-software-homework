using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    class StringGetter
    {
        public event Func<string, int> StringGenerated;

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
