using System;

namespace Compiler
{
    class ScanUndefinedException : Exception
    {
        public int Index { get; private set; }
        public char Character { get; private set; }

        public ScanUndefinedException(int index, char character) : base("Undefined symbol found")
        {
            Index = index;
            Character = character;
        }
    }
}
