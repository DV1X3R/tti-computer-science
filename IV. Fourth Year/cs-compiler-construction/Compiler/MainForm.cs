using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler
{
    public partial class MainForm : Form
    {
        private SourceCompiler sourceCompiler;

        public MainForm()
        {
            InitializeComponent();
            UpdateCompiler();
            CompileAndRefresh();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var reader = new System.IO.StreamReader(openFileDialog.OpenFile()))
                {
                    sourceTextBox.Text = reader.ReadToEnd();
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateCompiler()
        {
            var keywords = (from string s in keywordsListBox.Items select s).ToList();
            var delimiters1 = (from string s in delimiters1ListBox.Items select s).ToList();
            var delimiters2 = (from string s in delimiters2ListBox.Items select s).ToList();
            var stringDelimiter = stringDelimiterTextBox.Text.Length > 0 ? stringDelimiterTextBox.Text[0] : ' ';
            sourceCompiler = new SourceCompiler(keywords, stringDelimiter, delimiters1, delimiters2);
        }

        private void CompileAndRefresh()
        {
            var source = sourceTextBox.Text;
            try
            {
                sourceCompiler.Compile(source);
                toolStripStatusLabel.Text = string.Format("Scan successful!");
            }
            catch(ScannerException e)
            {
                var line = sourceTextBox.GetLineFromCharIndex(e.Index) + 1;
                var index = e.Index - sourceTextBox.GetFirstCharIndexFromLine(line - 1) + 1;
                toolStripStatusLabel.Text = string.Format("Scan failed: {0} [{1}] on line {2},{3}", e.Message, e.Character, line, index);
            }

            identifiersListBox.Items.Clear();
            identifiersListBox.Items.AddRange(sourceCompiler.Scanner.Identifiers.ToArray());

            integersListBox.Items.Clear();
            integersListBox.Items.AddRange(sourceCompiler.Scanner.Integers.ToArray());

            stringsListBox.Items.Clear();
            stringsListBox.Items.AddRange(sourceCompiler.Scanner.Strings.ToArray());

            scannerDataGridView.Rows.Clear();
            foreach (Lexeme lexeme in sourceCompiler.Scanner.Lexemes)
                scannerDataGridView.Rows.Add(lexeme.Type, lexeme.Index, lexeme.Value);
        }

        private void sourceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (autoScanToolStripMenuItem.Checked)
                CompileAndRefresh();
        }

        private void runScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileAndRefresh();
        }

        private void autoScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoScanToolStripMenuItem.Checked = !autoScanToolStripMenuItem.Checked;
        }

    }
}
