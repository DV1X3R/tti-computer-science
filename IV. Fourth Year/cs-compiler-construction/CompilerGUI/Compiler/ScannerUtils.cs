using System;
using System.Text;

namespace CompilerGUI.Compiler
{
    enum ScannerLogType
    {
        Start, Success, New, Concat, Push, EndOfFile, EndlessString, Undefined, Skip
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

    class ScannerException : Exception
    {
        public int Index { get; private set; }
        public char Character { get; private set; }

        public ScannerException(int index, char character, string message) : base(message)
        {
            Index = index;
            Character = character;
        }

    }
}
