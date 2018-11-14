using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    // 1. Лексический анализатор
    class Scanner
    {
        // Исходные данные
        public List<string> Keywords { get; private set; } = new List<string>();
        public List<string> Delimiters1 { get; private set; } = new List<string>();
        public List<string> Delimiters2 { get; private set; } = new List<string>();
        public char DelimiterString { get; private set; }

        // Заполняет лексический анализатор
        public List<Lexeme> Lexemes { get; private set; } = new List<Lexeme>();
        public List<string> Identifiers { get; private set; } = new List<string>();
        public List<string> Integers { get; private set; } = new List<string>();
        public List<string> Strings { get; private set; } = new List<string>();

        // Проверка на ключевые слова и делиметры
        private bool IsKeyword(string word) => Keywords.Contains(word);
        private bool IsStringDelimiter(char character) => DelimiterString.CompareTo(character) == 0;
        private bool IsDelimiter1(char character) => Delimiters1.Contains(character.ToString());
        private bool IsDelimiter2(string word) => Delimiters2.Contains(word);

        // Проверка на буквы и цифры
        private bool IsLetter(char character) => Char.IsLetter(character);
        private bool IsDigit(char character) => Char.IsDigit(character);
        private bool IsLetterOrDigit(char character) => Char.IsLetterOrDigit(character);

        public Scanner(List<string> keywords, char delimiterString, List<string> delimiters1, List<string> delimiters2)
        {
            this.Keywords = keywords;
            this.DelimiterString = delimiterString;
            this.Delimiters1 = delimiters1;
            this.Delimiters2 = delimiters2;
        }

        // Заполнение таблиц
        private void AddLexeme(LexemeType lexemeType, StringBuilder lexeme)
        {
            List<string> list = null;
            string value = lexeme.ToString();

            switch (lexemeType)
            {
                case LexemeType.KEY: list = Keywords; break;
                case LexemeType.DL1: list = Delimiters1; break;
                case LexemeType.DL2: list = Delimiters2; break;
                case LexemeType.IDN: list = Identifiers; break;
                case LexemeType.INT: list = Integers; break;
                case LexemeType.STR: list = Strings; break;
            }

            int index = lexemeType == LexemeType.DLS ? 0 : list.IndexOf(value);
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
            Integers.Clear();
            Strings.Clear();

            var lexeme = new StringBuilder();
            char character = ' ';
            int index = 0;
            bool GetCharacterResult = false;

            while (true)
            {
                lexeme.Clear(); // Новая лексема
                GetCharacterResult = GetCharacter(source, index, out character); // Считываем текущий символ
                if (!GetCharacterResult) // Если конец программы - break;
                    break;

                if (IsLetter(character)) // Первый символ - буква
                { // <идентификатор> ::= буква | <идентификатор> буква | <идентификатор> цифра
                    do
                    {
                        lexeme.Append(character); // Конкатенация
                        GetCharacterResult = GetCharacter(source, ++index, out character); // Считываем следующий символ
                    }
                    while (GetCharacterResult && IsLetterOrDigit(character));

                    if (IsKeyword(lexeme.ToString()))
                    { // Ключевое слово
                        AddLexeme(LexemeType.KEY, lexeme);
                    }
                    else
                    { // Идентификатор
                        AddLexeme(LexemeType.IDN, lexeme);
                    }
                }

                else if (IsDigit(character)) // Первый символ - цифра
                { // <целое> ::= цифра | <целое> цифра
                    do
                    {
                        lexeme.Append(character);
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }
                    while (GetCharacterResult && IsDigit(character));

                    if (IsLetter(character))
                        throw new ScanIntegerIdentifierException(index, character);

                    AddLexeme(LexemeType.INT, lexeme);
                }

                else if (IsStringDelimiter(character)) // String разделитель
                {
                    int stringStartIndex = index;
                    lexeme.Append(character);
                    AddLexeme(LexemeType.DLS, lexeme);
                    lexeme.Clear();

                    // Литерал
                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    while (GetCharacterResult && !IsStringDelimiter(character))
                    { // Если мы нашли символ, и это не StringDelimiter
                        lexeme.Append(character);
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }

                    AddLexeme(LexemeType.STR, lexeme);

                    if (GetCharacterResult)
                    { // Если нашли завершающий StringDelimiter
                        lexeme.Clear();
                        lexeme.Append(character);
                        AddLexeme(LexemeType.DLS, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    {
                        throw new ScanInfiniteStringException(stringStartIndex);
                    }
                }

                else if (IsDelimiter1(character))
                {
                    lexeme.Append(character);

                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    if (GetCharacterResult && IsDelimiter2(lexeme.ToString() + character))
                    { // Двухпозиционный разделитель
                        lexeme.Append(character);
                        AddLexeme(LexemeType.DL2, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    { // Однопозиционный разделитель
                        AddLexeme(LexemeType.DL1, lexeme);
                    }
                }

                else if (Char.IsWhiteSpace(character))
                {
                    index += 1;
                }

                else
                {
                    throw new ScanUndefinedSymbolException(index, character);
                }

            }
        }

    }
}
