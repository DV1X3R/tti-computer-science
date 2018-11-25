namespace CompilerGUI.Compiler
{
    enum ParserLogType
    {
        Start, Success, ThrowEOF, ThowNotFound, Ok
    }

    class ParserLog
    {
        public ParserLogType Type { get; private set; }
        public Lexeme Lexeme { get; private set; }
        public string Expected { get; private set; }
        public string Result { get; private set; }

        public ParserLog(ParserLogType type, Lexeme lexeme, string expected, string result)
        {
            Type = type;
            Lexeme = lexeme;
            Expected = expected;
            Result = result;
        }

    }
}
