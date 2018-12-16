using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(Function);
            thread.Start();
            thread.Join();

            for (int i = 1; i < 101; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0} ", i);
            }

            Console.WriteLine("Press any key to quit... ");
            Console.ReadLine();
        }

        static void Function()
        {
            for (int i = 1; i < 101; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0} ", i);
            }
        }
        
    }
}
