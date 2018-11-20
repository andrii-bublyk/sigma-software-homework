using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action
{
    class StringGetter
    {
        public event Action<string> StringGenerated;

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
