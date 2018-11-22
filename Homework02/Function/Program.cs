using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    class Program
    {
        static void Main(string[] args)
        {
            StringGetter stringGetter = new StringGetter();
            StringCollector stringCollector = new StringCollector();
            AlphaNumbericCollector alphaNumbericCollector = new AlphaNumbericCollector();

            stringGetter.StringGenerated += stringCollector.ProcessString;
            stringGetter.StringGenerated += alphaNumbericCollector.ProcessString;
            stringGetter.Run();

            Console.ReadKey();
        }
    }
}
