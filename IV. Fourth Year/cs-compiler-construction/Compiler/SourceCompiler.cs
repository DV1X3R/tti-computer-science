using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Compiler.Scanner;
using Compiler.Parser;

namespace Compiler
{
    public class SourceCompiler
    {
        public CompilerScanner Scanner { get; private set; }
        public CompilerParser Parser { get; private set; }

        public SourceCompiler(List<string> keywords, char delimiterString, List<string> delimiters1, List<string> delimiters2,
            Action<ScannerLog> scannerLogger, Action<ParserLog> parserLogger)
        {
            Scanner = new CompilerScanner(keywords, delimiterString, delimiters1, delimiters2, scannerLogger);
            Parser = new CompilerParser(parserLogger);
        }

        public void Compile(string source)
        {
            Scanner.Scan(source);
            Parser.Parse(Scanner.Lexemes);
        }

    }
}
