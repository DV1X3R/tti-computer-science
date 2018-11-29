using System.Collections.ObjectModel;

namespace CompilerGUI.Compiler
{
    class SourceCompiler
    {
        public Scanner Scanner { get; private set; }
        public Parser Parser { get; private set; }
        public WfpGenerator WfpGenerator { get; private set; }

        public SourceCompiler()
        {
            var keywords = new ObservableCollection<string>() { "procedure", "var", "Byte", "Char", "array", "of", "Longint", "String", "Begin", "if", "and", "then", "else", "End", "or" };
            var delimiters1 = new ObservableCollection<string>() { ".", ";", ":", "[", "]", "=", "(", ")", ",", "-", ">", "<" };
            var delimiters2 = new ObservableCollection<string>() { "..", ":=" };
            var delimiterString = '\'';

            Scanner = new Scanner(keywords, delimiters1, delimiters2, delimiterString);
            WfpGenerator = new WfpGenerator();
            Parser = new Parser(Scanner, WfpGenerator);
        }

        public void Compile(string source)
        {
            Scanner.Logs.Clear();
            Parser.Logs.Clear();
            WfpGenerator.Quads.Clear();
            Scanner.Scan(source);
            Parser.Parse();
        }

    }
}
