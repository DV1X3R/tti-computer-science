using System.Text;

namespace CompilerGUI.Compiler
{
    enum ScannerLogType
    {
        Start, Success, ThrowEOF, ThrowSTR, ThrowUndefined, Append, New, Write, Skip
    }

    class ScannerLog
    {
        public ScannerLogType Type { get; private set; }
        public string Lexeme { get; private set; }
        public LexemeType LexemeType { get; private set; }
        public int Index { get; private set; }
        public char Character { get; private set; }

        public ScannerLog(ScannerLogType type, StringBuilder lexeme, LexemeType lexemeType, int index, char character)
        {
            Type = type;
            Lexeme = lexeme?.ToString();
            LexemeType = lexemeType;
            Index = index;
            Character = character;
        }

    }
}
