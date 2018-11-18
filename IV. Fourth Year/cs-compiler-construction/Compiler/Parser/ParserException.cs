using System;

namespace Compiler.Parser
{
    public class ParserException : Exception
    {
        public ParserLog Log { get; private set; }

        public ParserException(ParserLog log) : base()
        {
            Log = log;
        }

    }
}
