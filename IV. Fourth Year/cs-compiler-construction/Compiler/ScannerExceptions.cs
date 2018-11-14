using System;

namespace Compiler
{
    abstract class ScannerException : Exception
    {
        public int Index { get; private set; }
        public char Character { get; private set; }

        public ScannerException(int index, char character, string message) : base(message)
        {
            Index = index;
            Character = character;
        }
    }

    class ScanUndefinedSymbolException : ScannerException
    {
        public ScanUndefinedSymbolException(int index, char character) : base(index, character, "Undefined symbol found") { }
    }

    class ScanIntegerIdentifierException : ScannerException
    {
        public ScanIntegerIdentifierException(int index, char character) : base(index, character, "Identifier starts with digit") { }
    }

    class ScanInfiniteStringException : ScannerException
    {
        public ScanInfiniteStringException(int index) : base(index, ' ', "String termination character cannot be found") { }
    }

}
