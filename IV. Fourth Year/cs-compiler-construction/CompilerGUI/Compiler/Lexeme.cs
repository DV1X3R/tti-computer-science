namespace CompilerGUI.Compiler
{
    enum LexemeType
    {
        NA, KEY, IDN, DL1, DL2, DLS, INT, STR
    }

    class Lexeme
    {
        public LexemeType Type { get; private set; }
        public int Index { get; private set; }
        public string Value { get; private set; }

        public Lexeme(LexemeType type, int index, string lexeme)
        {
            Type = type;
            Index = index;
            Value = lexeme;
        }
    }
}
