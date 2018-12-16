using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace EventSync
{
    class Program
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Thread t1 = new Thread(Write);
            Thread t2 = new Thread(Read);
            Thread t3 = new Thread(Count);

            t1.Start();
            t2.Start();
            t3.Start();

            Console.ReadLine();
        }

        static void Write()
        {
            var fs = new FileStream(@"sync.bin", FileMode.Create);
            var bw = new BinaryWriter(fs);

            for (int i = 1; i < 101; i++)
            {
                bw.Write(i);
            }

            bw.Close();
            mre.Set();
        }

        static void Read()
        {
            mre.WaitOne();
            var fs = new FileStream(@"sync.bin", FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);

            for (int i = 0; i < 100; i++)
            {
                Console.Write("{0} ", br.ReadInt32());
            }

            br.Close();
        }

        static void Count()
        {
            mre.WaitOne();
            var fs = new FileStream(@"sync.bin", FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);

            int s = 0;
            for (int i = 0; i < 100; i++)
            {
                s += br.ReadInt32();
            }

            br.Close();
            Console.WriteLine("Sum: {0}", s);
        }

    }
}
