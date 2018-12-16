using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NamedMutex
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Mutex(false, "NamedMutex");
            m.WaitOne();
            Console.WriteLine("Mutex acquired> Press any key to quit...");
            Console.ReadLine();
            m.ReleaseMutex();
        }
    }
}
