using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework02
{

    class StringGetter
    {
        public delegate void HandlerDelegate(string str);
        public event HandlerDelegate StringGenerated;

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
