using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    class SourceCompiler
    {
        // Исходные данные
        public IReadOnlyCollection<string> Keywords { get { return Array.AsReadOnly(keywords.ToArray()); } }
        public IReadOnlyCollection<string> Delimiters1 { get { return Array.AsReadOnly(delimiters1.ToArray()); } }
        public IReadOnlyCollection<string> Delimiters2 { get { return Array.AsReadOnly(delimiters2.ToArray()); } }
        public char DelimiterString { get { return delimiterString; } }

        private List<string> keywords = new List<string>();
        private List<string> delimiters1 = new List<string>();
        private List<string> delimiters2 = new List<string>();
        private char delimiterString;

        // Заполняет лексический анализатор
        public IReadOnlyCollection<Lexeme> Lexemes { get { return Array.AsReadOnly(lexemes.ToArray()); } }
        public IReadOnlyCollection<string> Identifiers { get { return Array.AsReadOnly(identifiers.ToArray()); } }
        public IReadOnlyCollection<string> Integers { get { return Array.AsReadOnly(integers.ToArray()); } }
        public IReadOnlyCollection<string> Strings { get { return Array.AsReadOnly(strings.ToArray()); } }

        private List<Lexeme> lexemes = new List<Lexeme>();
        private List<string> identifiers = new List<string>();
        private List<string> integers = new List<string>();
        private List<string> strings = new List<string>();

        public SourceCompiler(string[] keywords, char delimiterString, string[] delimiters1, string[] delimiters2)
        {
            this.keywords = keywords.ToList();
            this.delimiterString = delimiterString;
            this.delimiters1 = delimiters1.ToList();
            this.delimiters2 = delimiters2.ToList();
        }

        public void Compile(string source)
        {
            source = (string)source.Clone();
            Scan(source);
        }

        // 1. Лексический анализатор

        // Проверка на ключевые слова и делиметры
        private bool IsKeyword(string word) => keywords.Contains(word);
        private bool IsStringDelimiter(char character) => delimiterString.CompareTo(character) == 0;
        private bool IsDelimiter1(string character) => delimiters1.Contains(character);
        private bool IsDelimiter2(string word) => delimiters2.Contains(word);

        // Проверка на буквы и цифры
        private bool IsLetter(char character) => Char.IsLetter(character);
        private bool IsDigit(char character) => Char.IsDigit(character);
        private bool IsLetterOrDigit(char character) => Char.IsLetterOrDigit(character);

        // Заполнение таблиц
        private void AddLexeme(LexemeType lexemeType, string lexeme)
        {
            List<string> list = null;

            switch (lexemeType)
            {
                case LexemeType.KEY: list = keywords; break;
                case LexemeType.DL1: list = delimiters1; break;
                case LexemeType.DL2: list = delimiters2; break;
                case LexemeType.IDN: list = identifiers; break;
                case LexemeType.INT: list = integers; break;
                case LexemeType.STR: list = strings; break;
            }

            int index = list.IndexOf(lexeme);
            if (index == -1)
            {
                list.Add(lexeme);
                index = list.Count - 1;
            }

            lexemes.Add(new Lexeme(lexemeType, index, lexeme));
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
        private void Scan(string source)
        {
            lexemes.Clear();
            identifiers.Clear();
            integers.Clear();
            strings.Clear();

            var lexeme = new StringBuilder();
            char character = ' ';
            int index = 0;

            while (true)
            {
                lexeme.Clear(); // Новая лексема
                if (!GetCharacter(source, index, out character)) // Считываем первый символ
                    break; // Если конец программы - break;

                if (IsLetter(character)) // Первый символ - буква
                { // <идентификатор> ::= буква | <идентификатор> буква | <идентификатор> цифра
                    do { lexeme.Append(character); } // Конкатенация
                    while (GetCharacter(source, ++index, out character) && IsLetterOrDigit(character));

                    if (IsKeyword(lexeme.ToString()))
                    { // Проверка на ключевое слово
                        AddLexeme(LexemeType.KEY, lexeme.ToString());
                    }
                    else
                    { // Иначе идентификатор
                        AddLexeme(LexemeType.IDN, lexeme.ToString());
                    }
                }

                else if (IsDigit(character)) // Первый символ - цифра
                { // <целое> ::= цифра | <целое> цифра
                    do { lexeme.Append(character); }
                    while (GetCharacter(source, ++index, out character) && IsDigit(character));
                    AddLexeme(LexemeType.INT, lexeme.ToString());
                }

                else if (IsDelimiter1(character.ToString()))
                {
                    lexeme.Append(character);
                    bool GetCharacterResult = GetCharacter(source, ++index, out character);
                    if (GetCharacterResult && IsDelimiter2(lexeme.ToString() + character))
                    { // Двухпозиционный разделитель
                        lexeme.Append(character);
                        index += 1; // Переход на следующий символ
                        AddLexeme(LexemeType.DL2, lexeme.ToString());
                    }
                    else
                    { // Однопозиционный разделитель
                        AddLexeme(LexemeType.DL1, lexeme.ToString());

                        if(GetCharacterResult && IsStringDelimiter(lexeme.ToString()[0]))
                        { // Литерал
                            lexeme.Clear();

                            while(GetCharacterResult && !IsStringDelimiter(character))
                            { // Если мы нашли символ, и это не StringDelimiter
                                lexeme.Append(character);
                                GetCharacterResult = GetCharacter(source, ++index, out character);
                            }

                            AddLexeme(LexemeType.STR, lexeme.ToString());

                            if(GetCharacterResult)
                            { // Если нашли завершающий StringDelimiter
                                lexeme.Clear();
                                lexeme.Append(character);
                                AddLexeme(LexemeType.DL1, lexeme.ToString());
                                index += 1; // Переход на следующий символ
                            }

                        }
                    }
                }

                else // White Space
                {
                    index += 1;
                }
            }
        }

    }
}
