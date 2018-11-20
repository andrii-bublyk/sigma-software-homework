using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    delegate void HandlerDelegate(string str);
    class StringGetter
    {
        public HandlerDelegate StringGenerated;
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
