using System.Collections.ObjectModel;

namespace CompilerGUI.Compiler
{
    class SourceCompiler
    {
        public Scanner Scanner { get; private set; }
        public Parser Parser { get; private set; }

        public SourceCompiler(ObservableCollection<string> keywords, ObservableCollection<string> delimiters1, ObservableCollection<string> delimiters2, char delimiterString)
        {
            Scanner = new Scanner(keywords, delimiters1, delimiters2, delimiterString);
            Parser = new Parser(Scanner);
        }

        public void Compile(string source)
        {
            Scanner.Logs.Clear();
            Parser.Logs.Clear();
            Scanner.Scan(source);
            Parser.Parse();
        }

    }
}
