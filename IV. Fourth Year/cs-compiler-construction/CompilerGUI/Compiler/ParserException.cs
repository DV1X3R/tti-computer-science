using System;

namespace CompilerGUI.Compiler
{
    class ParserException : Exception
    {
        public ParserLog Log { get; private set; }

        public ParserException(ParserLog log) : base()
        {
            Log = log;
        }

    }
}
