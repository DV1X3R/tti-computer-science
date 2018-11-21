using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputThird
{
    public class Class1
    {
        public int GetInput()
        {
            int i = 0;
            bool success = false;

            do
            {
                Console.Write("C#: Please enter an integer> ");
                success = Int32.TryParse(Console.ReadLine(), out i);
            } while (!success);

            return i;
        }
    }
}
