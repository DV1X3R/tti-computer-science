using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CompilerGUI.Compiler
{
    class CompilerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private SourceCompiler sourceCompiler;

        public CompilerViewModel()
        {
            sourceCompiler = new SourceCompiler();

            sourceCodeText =
                "procedure TIndicator.Draw;\n" + "var\n\t" + "Color: Byte; Frame: Char;\n\t" + "L: array[0..1] of Longint;\n\t" + "S: String[15];\n\t" +
                "B: TDrawBuffer;\n" + "Begin\n\t" + "if State and sfDragging = 0 then Color := GetColor(1);\n\t" + "else\n\t" + "MoveChar(B, Frame, Color, Size.X);\n\t" +
                "if Modified then WordRec(B[0].Lo := 15);\n\t" + "FormatStr(S, ' %d:%d ', L);\n\t" + "MoveStr(B[8 - Pos(':', S)], S, Color);\n\t" + "WriteBuf(0, 0, Size.X, 1, B);\n" + "End;";
        }

        public ICommand CompileCommand { get { return new DelegateCommand((obj) => { Compile(); }); } }

        private void Compile()
        {
            ScannerResultText = "[ok]";
            ScannerResultColor = "LightGreen";
            ParserResultText = "[ok]";
            ParserResultColor = "LightGreen";
            StatusText = "> Compilation Completed Successfully  |  [ok]";

            try { sourceCompiler.Compile(SourceCodeText); }
            catch (ScannerException e)
            {
                ScannerResultText = "[failure]";
                ScannerResultColor = "Red";
                ParserResultText = "[failure]";
                ParserResultColor = "Red";

                //var line = tbSourceCode.GetLineIndexFromCharacterIndex(e.Index) + 1;
                //var index = e.Index - tbSourceCode.GetCharacterIndexFromLineIndex(line - 1) + 1;
                StatusText = $"> Scan failed: {e.Message} [{e.Character}] on {e.Index} index";
            }
            catch (ParserException e)
            {
                ParserResultText = "[failure]";
                ParserResultColor = "Red";

                StatusText = $"> Parsing failed on lexeme: {e.Log.Lexeme?.Value}. {e.Log.Expected} expected";
            }

            Logs.Clear();

            foreach (var log in sourceCompiler.Scanner.Logs)
            {
                string msg = "Scanner> ";
                msg += ("[" + log.Type.ToString() + "]").PadRight(12);
                msg += " Index: " + log.Index.ToString().PadRight(5);
                msg += " Character: [" + (Char.IsWhiteSpace(log.Character) ? " " : log.Character.ToString()) + "]  ";
                msg += " Lexeme: " + ("[" + log.Lexeme?.ToString() + "]").PadRight(13);
                msg += " Type: " + log.LexemeType;
                Logs.Insert(0, msg);
            }

            foreach (var log in sourceCompiler.Parser.Logs)
            {
                string msg = "Parser> ";
                msg += ("[" + log.Type.ToString() + "]").PadRight(15);
                msg += " Lexeme: " + ("[" + log.Lexeme?.Value + "]").PadRight(13);
                msg += " Expected: " + log.Expected?.PadRight(25);
                msg += " Result: " + log.Result;
                Logs.Insert(0, msg);
            }

        }

        public ObservableCollection<string> Logs { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<Lexeme> Lexemes { get { return sourceCompiler.Scanner.Lexemes; } }
        public ObservableCollection<string> Keywords { get { return sourceCompiler.Scanner.Keywords; } }
        public ObservableCollection<string> Identifiers { get { return sourceCompiler.Scanner.Identifiers; } }
        public ObservableCollection<string> Literals { get { return sourceCompiler.Scanner.Literals; } }
        public ObservableCollection<Quad> Quads { get { return sourceCompiler.WfpGenerator.Quads; } }

        private string sourceCodeText;
        public string SourceCodeText
        {
            get { return sourceCodeText; }
            set
            {
                sourceCodeText = value;
                OnPropertyChanged("SourceCodeText");
            }
        }

        private string scannerResultText;
        public string ScannerResultText
        {
            get { return scannerResultText; }
            set
            {
                scannerResultText = value;
                OnPropertyChanged("ScannerResultText");
            }
        }

        private string scannerResultColor;
        public string ScannerResultColor
        {
            get { return scannerResultColor; }
            set
            {
                scannerResultColor = value;
                OnPropertyChanged("ScannerResultColor");
            }
        }

        private string parserResultText;
        public string ParserResultText
        {
            get { return parserResultText; }
            set
            {
                parserResultText = value;
                OnPropertyChanged("ParserResultText");
            }
        }

        private string parserResultColor;
        public string ParserResultColor
        {
            get { return parserResultColor; }
            set
            {
                parserResultColor = value;
                OnPropertyChanged("ParserResultColor");
            }
        }

        private string statusText;
        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnPropertyChanged("StatusText");
            }
        }
    }

    class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
