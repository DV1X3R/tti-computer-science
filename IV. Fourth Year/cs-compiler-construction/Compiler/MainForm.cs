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
            var keywords = (from string s in keywordsListBox.Items select s).ToArray();
            var delimiters1 = (from string s in delimiters1ListBox.Items select s).ToArray();
            var delimiters2 = (from string s in delimiters2ListBox.Items select s).ToArray();
            var stringDelimiter = stringDelimiterTextBox.Text.Length > 0 ? stringDelimiterTextBox.Text[0] : ' ';
            sourceCompiler = new SourceCompiler(keywords, stringDelimiter, delimiters1, delimiters2);
        }

        private void CompileAndRefresh()
        {
            var source = sourceTextBox.Text;
            sourceCompiler.Compile(source);

            identifiersListBox.Items.Clear();
            identifiersListBox.Items.AddRange(sourceCompiler.Identifiers.ToArray());

            integersListBox.Items.Clear();
            integersListBox.Items.AddRange(sourceCompiler.Integers.ToArray());

            stringsListBox.Items.Clear();
            stringsListBox.Items.AddRange(sourceCompiler.Strings.ToArray());

            scannerDataGridView.Rows.Clear();
            foreach (Lexeme lexeme in sourceCompiler.Lexemes)
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
