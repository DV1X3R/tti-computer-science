using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    class SourceCompiler
    {
        public Scanner Scanner { get; private set; }
        public Parser Parser { get; private set; }

        public SourceCompiler(List<string> keywords, char delimiterString, List<string> delimiters1, List<string> delimiters2)
        {
            Scanner = new Scanner(keywords, delimiterString, delimiters1, delimiters2);
        }

        public void Compile(string source)
        {
            Scanner.Scan(source);
        }

    }
}
