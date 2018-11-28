using System;
using System.Collections.ObjectModel;
using System.Text;

namespace CompilerGUI.Compiler
{
    // 1. Лексический анализатор
    class Scanner
    {
        // Логи для отладки
        public ObservableCollection<ScannerLog> Logs { get; private set; } = new ObservableCollection<ScannerLog>();

        // Исходные данные
        private ObservableCollection<string> keywords = new ObservableCollection<string>();
        private ObservableCollection<string> delimiters1 = new ObservableCollection<string>();
        private ObservableCollection<string> delimiters2 = new ObservableCollection<string>();
        private char delimiterString;

        // Общая таблица ключевых слов
        public ObservableCollection<string> Keywords { get; private set; } = new ObservableCollection<string>();

        // Заполняет сканнер
        public ObservableCollection<Lexeme> Lexemes { get; private set; } = new ObservableCollection<Lexeme>();
        public ObservableCollection<string> Identifiers { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Literals { get; private set; } = new ObservableCollection<string>();

        // Проверка на ключевые слова и делиметры
        private bool IsKeyword(string word) => keywords.Contains(word);
        private bool IsDelimiter1(string character) => delimiters1.Contains(character);
        private bool IsDelimiter2(string word) => delimiters2.Contains(word);
        private bool IsStringDelimiter(char character) => delimiterString.Equals(character);

        // Проверка на буквы и цифры
        private bool IsLetter(char character) => Char.IsLetter(character);
        private bool IsDigit(char character) => Char.IsDigit(character);
        private bool IsLetterOrDigit(char character) => Char.IsLetterOrDigit(character);

        public Scanner(ObservableCollection<string> keywords, ObservableCollection<string> delimiters1, ObservableCollection<string> delimiters2, char delimiterString)
        {
            this.keywords = keywords;
            this.delimiters1 = delimiters1;
            this.delimiters2 = delimiters2;
            this.delimiterString = delimiterString;

            foreach (var word in keywords)
                Keywords.Add(word);
            foreach (var word in delimiters1)
                Keywords.Add(word);
            foreach (var word in delimiters2)
                Keywords.Add(word);

            Keywords.Add(delimiterString.ToString());
        }

        // Заполнение таблиц
        private void AddLexeme(LexemeType lexemeType, StringBuilder lexeme)
        {
            ObservableCollection<string> list = null;
            string value = lexeme.ToString();

            switch (lexemeType)
            {
                case LexemeType.KEY: case LexemeType.DL1: case LexemeType.DL2: case LexemeType.DLS: list = Keywords; break;
                case LexemeType.IDN: list = Identifiers; break;
                case LexemeType.INT: case LexemeType.STR: list = Literals; break;
                default: throw new ArgumentOutOfRangeException($"Lexeme Type '{lexemeType}' is not supported");
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
            source = source.Replace('\n', ' ').Replace('\t', ' ');

            Logs.Clear();
            Lexemes.Clear();
            Identifiers.Clear();
            Literals.Clear();

            var lexeme = new StringBuilder();
            char character = ' ';
            int index = 0;
            bool GetCharacterResult = false;

            Logs.Add(new ScannerLog(ScannerLogType.Start, null, LexemeType.NA, index, character));

            while (true)
            {
                lexeme.Clear(); // Новая лексема
                GetCharacterResult = GetCharacter(source, index, out character); // Считываем текущий символ
                if (!GetCharacterResult) // Если конец программы - break;
                {
                    Logs.Add(new ScannerLog(ScannerLogType.EndOfFile, lexeme, LexemeType.NA, index, character));
                    break;
                }

                if (IsLetter(character)) // Первый символ - буква
                { // <идентификатор> ::= буква | <идентификатор> буква | <идентификатор> цифра
                    Logs.Add(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.IDN, index, character));
                    do
                    {
                        lexeme.Append(character); // Конкатенация
                        Logs.Add(new ScannerLog(ScannerLogType.Concat, lexeme, LexemeType.IDN, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character); // Считываем следующий символ
                    }
                    while (GetCharacterResult && IsLetterOrDigit(character));

                    if (IsKeyword(lexeme.ToString()))
                    { // Ключевое слово
                        Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.KEY, index, character));
                        AddLexeme(LexemeType.KEY, lexeme);
                    }
                    else
                    { // Идентификатор
                        Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.IDN, index, character));
                        AddLexeme(LexemeType.IDN, lexeme);
                    }
                }

                else if (IsDigit(character)) // Первый символ - цифра
                { // <целое> ::= цифра | <целое> цифра
                    Logs.Add(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.INT, index, character));
                    do
                    {
                        lexeme.Append(character);
                        Logs.Add(new ScannerLog(ScannerLogType.Concat, lexeme, LexemeType.INT, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }
                    while (GetCharacterResult && IsDigit(character));

                    Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.INT, index, character));
                    AddLexeme(LexemeType.INT, lexeme);
                }

                else if (IsStringDelimiter(character)) // String разделитель
                {
                    int stringStartIndex = index;
                    lexeme.Append(character);
                    Logs.Add(new ScannerLog(ScannerLogType.Concat, lexeme, LexemeType.DLS, index, character));
                    AddLexeme(LexemeType.DLS, lexeme);
                    lexeme.Clear();

                    // Литерал
                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    while (GetCharacterResult && !IsStringDelimiter(character))
                    { // Если мы нашли символ, и это не StringDelimiter
                        lexeme.Append(character);
                        Logs.Add(new ScannerLog(ScannerLogType.Concat, lexeme, LexemeType.STR, index, character));
                        GetCharacterResult = GetCharacter(source, ++index, out character);
                    }

                    Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.STR, index, character));
                    AddLexeme(LexemeType.STR, lexeme);

                    if (GetCharacterResult)
                    { // Если нашли завершающий StringDelimiter
                        lexeme.Clear();
                        lexeme.Append(character);
                        Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.DLS, index, character));
                        AddLexeme(LexemeType.DLS, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    {
                        Logs.Add(new ScannerLog(ScannerLogType.EndlessString, lexeme, LexemeType.DLS, index, character));
                        throw new ScannerException(stringStartIndex, ' ', "String termination character cannot be found");
                    }
                }

                else if (IsDelimiter1(character.ToString()))
                {
                    lexeme.Append(character);
                    Logs.Add(new ScannerLog(ScannerLogType.New, lexeme, LexemeType.DL1, index, character));

                    GetCharacterResult = GetCharacter(source, ++index, out character);
                    if (GetCharacterResult && IsDelimiter2(lexeme.ToString() + character))
                    { // Двухпозиционный разделитель
                        lexeme.Append(character);
                        Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.DL2, index, character));
                        AddLexeme(LexemeType.DL2, lexeme);
                        index += 1; // Переход на следующий символ
                    }
                    else
                    { // Однопозиционный разделитель
                        Logs.Add(new ScannerLog(ScannerLogType.Push, lexeme, LexemeType.DL1, index, character));
                        AddLexeme(LexemeType.DL1, lexeme);
                    }
                }

                else if (Char.IsWhiteSpace(character))
                {
                    Logs.Add(new ScannerLog(ScannerLogType.Skip, lexeme, LexemeType.NA, index, character));
                    index += 1;
                }

                else
                {
                    Logs.Add(new ScannerLog(ScannerLogType.Undefined, lexeme, LexemeType.NA, index, character));
                    throw new ScannerException(index, character, "Undefined symbol found");
                }
            }

            Logs.Add(new ScannerLog(ScannerLogType.Success, lexeme, LexemeType.NA, index, character));
        }

    }
}
