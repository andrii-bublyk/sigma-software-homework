using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework01
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<int> myLS = new MyLinkedList<int>();
            myLS.Add(1);
            myLS.Add(2);
            myLS.Add(5);
            Console.WriteLine("Manual Add result:");
            Console.WriteLine(myLS[0]);
            Console.WriteLine(myLS[1]);
            Console.WriteLine(myLS[2]);

            Console.WriteLine();
            Console.WriteLine("Remove at index 1 result:");
            myLS.RemoveAt(1);
            foreach (var i in myLS)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Insert at index 1 result:");
            myLS.InsertAt(1, 8);
            foreach (var i in myLS)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Remove at index 0 result:");
            myLS.RemoveAt(0);
            foreach (var i in myLS)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Insert at index 0 result:");
            myLS.InsertAt(0, 9);
            foreach (var i in myLS)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Method ToArray result:");
            int[] arr = myLS.ToArray();
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}
