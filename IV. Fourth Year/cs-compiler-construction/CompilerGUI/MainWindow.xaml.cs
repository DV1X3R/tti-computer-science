using System;
using System.Collections.Generic;
using System.Windows;

using Compiler;
using Compiler.Scanner;
using Compiler.Parser;

namespace CompilerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCompiler();
        }

        private SourceCompiler sourceCompiler;

        private void InitializeCompiler()
        {
            var keywords = new List<string>() { "procedure", "var", "Byte", "Char", "array", "of", "Longint", "String", "Begin", "if", "and", "then", "else", "End", "or" };
            var delimiterString = '\'';
            var delimiters1 = new List<string>() { ".", ";", ":", "[", "]", "=", "(", ")", ",", "-", ">", "<" };
            var delimiters2 = new List<string>() { "..", ":=" };

            sourceCompiler = new SourceCompiler(keywords, delimiterString, delimiters1, delimiters2, ScannerLogger, ParserLogger);

            dgLexemes.ItemsSource = sourceCompiler.Scanner.Lexemes;
            foreach (string keyword in sourceCompiler.Scanner.Keywords)
                lbKeywords.Items.Add(keyword);

            tbSourceCode.TextChanged += Compile_Click;

            Compile();
        }

        private void ScannerLogger(ScannerLog log)
        {
            string msg = "Scanner> ";
            msg += ("[" + log.Type.ToString() + "]").PadRight(12);
            msg += " Index: " + log.Index.ToString().PadRight(5);
            msg += " Character: [" + (Char.IsWhiteSpace(log.Character) ? " " : log.Character.ToString()) + "]  ";
            msg += " Lexeme: " + ("[" + log.Lexeme + "]").PadRight(12);
            msg += " Type: " + log.LexemeType;
            lbDebugOutput.Items.Insert(0, msg);
            tbStatus.Text = "Scanner> " + log.Type.ToString();
        }

        private void ParserLogger(ParserLog log)
        {
            string msg = "Parser> ";
            msg += ("[" + log.Type.ToString() + "]").PadRight(15);
            msg += " Lexeme: " + ("[" + log.Lexeme?.Value + "]").PadRight(12);
            msg += " Expected: " + log.Expected?.PadRight(25);
            msg += " Result: " + log.Result;
            lbDebugOutput.Items.Insert(0, msg);
            tbStatus.Text = "Parser> " + log.Type.ToString();
        }

        private void Compile()
        {
            lbDebugOutput.Items.Clear();
            lbIdentifiers.Items.Clear();
            lbLiterals.Items.Clear();

            lScannerResult.Content = "[ok]";
            lScannerResult.Foreground = System.Windows.Media.Brushes.LightGreen;

            lParserResult.Content = "[ok]";
            lParserResult.Foreground = System.Windows.Media.Brushes.LightGreen;

            try { sourceCompiler.Compile(tbSourceCode.Text); }
            catch (ScannerException e)
            {
                lScannerResult.Content = "[failure]";
                lScannerResult.Foreground = System.Windows.Media.Brushes.Red;
                lParserResult.Content = "[failure]";
                lParserResult.Foreground = System.Windows.Media.Brushes.Red;

                var line = tbSourceCode.GetLineIndexFromCharacterIndex(e.Index) + 1;
                var index = e.Index - tbSourceCode.GetCharacterIndexFromLineIndex(line - 1) + 1;
                tbStatus.Text = string.Format("Scan failed: {0} [{1}] on line {2},{3}", e.Message, e.Character, line, index);
            }
            catch (ParserException e)
            {
                lParserResult.Content = "[failure]";
                lParserResult.Foreground = System.Windows.Media.Brushes.Red;
                tbStatus.Text = string.Format("Parsing failed on lexeme: {0}. {1} expected", e.Log.Lexeme?.Value, e.Log.Expected);
            }

            dgLexemes.Items.Refresh();
            foreach (string identifier in sourceCompiler.Scanner.Identifiers)
                lbIdentifiers.Items.Add(identifier);
            foreach (string literal in sourceCompiler.Scanner.Literals)
                lbLiterals.Items.Add(literal);

        }

        private void Compile_Click(object sender, RoutedEventArgs e)
            => Compile();

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void AutoCompile_Click(object sender, RoutedEventArgs e)
        {
            if (bnAuto.IsChecked)
                tbSourceCode.TextChanged -= Compile_Click;
            else
                tbSourceCode.TextChanged += Compile_Click;

            bnAuto.IsChecked = !bnAuto.IsChecked;
        }

    }
}
