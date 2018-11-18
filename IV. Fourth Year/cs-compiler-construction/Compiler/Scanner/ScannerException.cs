using System;

namespace Compiler.Scanner
{
    public class ScannerException : Exception
    {
        public int Index { get; private set; }
        public char Character { get; private set; }

        public ScannerException(int index, char character, string message) : base(message)
        {
            Index = index;
            Character = character;
        }
    }
}
