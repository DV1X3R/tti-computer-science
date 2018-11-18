using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Scanner
{
    // 1. Лексический анализатор
    public class CompilerScanner
    {
        // Исходные данные
        private List<string> keywords = new List<string>();
        private List<string> delimiters1 = new List<string>();
        private List<string> delimiters2 = new List<string>();
        private char delimiterString;

        // Заполняет лексический анализатор
        private List<string> integers = new List<string>();
        private List<string> strings = new List<string>();

        public List<Lexeme> Lexemes { get; private set; } = new List<Lexeme>();
        public List<string> Identifiers { get; private set; } = new List<string>();
        public List<string> Keywords { get; private set; } = new List<string>();
        public List<string> Literals { get; private set; } = new List<string>();

        // Проверка на ключевые слова и делиметры
        private bool IsKeyword(string word) => keywords.Contains(word);
        private bool IsStringDelimiter(char character) => delimiterString.CompareTo(character) == 0;
        private bool IsDelimiter1(char character) => delimiters1.Contains(character.ToString());
        private bool IsDelimiter2(string word) => delimiters2.Contains(word);

        // Проверка на буквы и цифры
        private bool IsLetter(char character) => Char.IsLetter(character);
        private bool IsDigit(char character) => Char.IsDigit(character);
        private bool IsLetterOrDigit(char character) => Char.IsLetterOrDigit(character);

        private Action<ScannerLog> logger;

        public CompilerScanner(List<string> keywords, char delimiterString, List<string> delimiters1, List<string> delimiters2, Action<ScannerLog> logger)
        {
            this.keywords = keywords;
            this.delimiterString = delimiterString;
            this.delimiters1 = delimiters1;
            this.delimiters2 = delimiters2;
            this.logger = logger ?? (x => { });

            Keywords.AddRange(keywords);
            Keywords.Add(delimiterString.ToString());
            Keywords.AddRange(delimiters1);
            Keywords.AddRange(delimiters2);

        }

        // Заполнение таблиц
        private void AddLexeme(LexemeType lexemeType, StringBuilder lexeme)
        {
            List<string> list = null;
            string value = lexeme.ToString();

            switch (lexemeType)
            {
                case LexemeType.KEY: case LexemeType.DL1: case LexemeType.DL2: case LexemeType.DLS:
                    list = Keywords; break;
                case LexemeType.IDN:
                    list = Identifiers; break;
                case LexemeType.INT: case LexemeType.STR:
                    list = Literals; break;
            }

            int index = list.IndexOf(value);
            if (index == -1)
            {
                list.Add(value);
                index = list.Count - 1;
            }

            Lexemes.Add(new Lexeme(lexemeType, index, value));
        }

        // Считывание символа
        private bool GetCharacter(string source, int index, out char character)
        {
            if (index < source.Length)
            {
                character = source[index];
                return true;
            }
            else
            {
                character = ' ';
                return false;
            }
        }

        // Сканирование
        public void Scan(string source)
        {
            Lexemes.Clear();
            Identifiers.Clear();
            Literals.Clear();
            integers.Clear();
            strings.Clear();

            var lexeme = new StringBuilder();
            char character = ' ';
            int index = 0;
            bool GetCharacterResult = false;

            logger(new ScannerLog(ScannerLogType.Start, null, LexemeType.UNKNOWN, index, character));

            while (true)
            {
                lexeme.Clear(); // Новая лексема
                GetCharacterResult = GetCharacter(source, index, out character); // Считываем текущий символ
                if (!GetCharacterResult) // Если конец программы - break;
                {
                    logger(new ScannerLog(ScannerLogType.ThrowEOF, lexeme, LexemeType.UNKNOWN, index, character));
                    break;
                }

                if (IsLetter(character)) // Первый символ - буква
                { // <идентификатор> ::= буква | <идентификатор> буква | <идентификатор> цифра
                    //logger(FormatLog("New KEY or IDN", lexeme, index, character));
                    logger(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.IDN, index, character));
                    do
                    {
                        lexeme.Append(character); // Конкатенация
                        logger(new ScannerLog(ScannerLogType.Append, lexeme, LexemeType.IDN, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character); // Считываем следующий символ
                    }
                    while (GetCharacterResult && IsLetterOrDigit(character));

                    if (IsKeyword(lexeme.ToString()))
                    { // Ключевое слово
                        logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.KEY, index, character));
                        AddLexeme(LexemeType.KEY, lexeme);
                    }
                    else
                    { // Идентификатор
                        logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.IDN, index, character));
                        AddLexeme(LexemeType.IDN, lexeme);
                    }
                }

                else if (IsDigit(character)) // Первый символ - цифра
                { // <целое> ::= цифра | <целое> цифра
                    logger(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.INT, index, character));
                    do
                    {
                        lexeme.Append(character);
                        logger(new ScannerLog(ScannerLogType.Append, lexeme, LexemeType.INT, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }
                    while (GetCharacterResult && IsDigit(character));

                    //if (IsLetter(character))
                    //throw new ScannerException(index, character, "Identifier starts with digit");

                    logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.INT, index, character));
                    AddLexeme(LexemeType.INT, lexeme);
                }

                else if (IsStringDelimiter(character)) // String разделитель
                {
                    int stringStartIndex = index;
                    lexeme.Append(character);
                    logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.DLS, index, character));
                    AddLexeme(LexemeType.DLS, lexeme);
                    lexeme.Clear();

                    // Литерал
                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    while (GetCharacterResult && !IsStringDelimiter(character))
                    { // Если мы нашли символ, и это не StringDelimiter
                        lexeme.Append(character);
                        logger(new ScannerLog(ScannerLogType.Append, lexeme, LexemeType.STR, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }

                    logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.STR, index, character));
                    AddLexeme(LexemeType.STR, lexeme);

                    if (GetCharacterResult)
                    { // Если нашли завершающий StringDelimiter
                        lexeme.Clear();
                        lexeme.Append(character);
                        logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.DLS, index, character));
                        AddLexeme(LexemeType.DLS, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    {
                        logger(new ScannerLog(ScannerLogType.ThrowSTR, lexeme, LexemeType.DLS, index, character));
                        throw new ScannerException(stringStartIndex, ' ', "String termination character cannot be found");
                    }
                }

                else if (IsDelimiter1(character))
                {
                    lexeme.Append(character);
                    logger(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.DL1, index, character));

                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    if (GetCharacterResult && IsDelimiter2(lexeme.ToString() + character))
                    { // Двухпозиционный разделитель
                        lexeme.Append(character);
                        logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.DL2, index, character));
                        AddLexeme(LexemeType.DL2, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    { // Однопозиционный разделитель
                        logger(new ScannerLog(ScannerLogType.Write, lexeme, LexemeType.DL1, index, character));
                        AddLexeme(LexemeType.DL1, lexeme);
                    }
                }

                else if (Char.IsWhiteSpace(character))
                {
                    logger(new ScannerLog(ScannerLogType.Skip, lexeme, LexemeType.UNKNOWN, index, character));
                    index += 1;
                }

                else
                {
                    logger(new ScannerLog(ScannerLogType.ThrowUndefined, lexeme, LexemeType.UNKNOWN, index, character));
                    throw new ScannerException(index, character, "Undefined symbol found");
                }
            }

            logger(new ScannerLog(ScannerLogType.Success, lexeme, LexemeType.UNKNOWN, index, character));
        }

    }
}
