namespace Compiler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.sourceGroupBox = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.keywordsGroupBox = new System.Windows.Forms.GroupBox();
            this.keywordsListBox = new System.Windows.Forms.ListBox();
            this.stringsGroupBox = new System.Windows.Forms.GroupBox();
            this.stringsListBox = new System.Windows.Forms.ListBox();
            this.integersGroupBox = new System.Windows.Forms.GroupBox();
            this.integersListBox = new System.Windows.Forms.ListBox();
            this.identifiersGroupBox = new System.Windows.Forms.GroupBox();
            this.identifiersListBox = new System.Windows.Forms.ListBox();
            this.stringDelimiterTextBox = new System.Windows.Forms.MaskedTextBox();
            this.delimiters1GroupBox = new System.Windows.Forms.GroupBox();
            this.delimiters1ListBox = new System.Windows.Forms.ListBox();
            this.delimiters2GroupBox = new System.Windows.Forms.GroupBox();
            this.delimiters2ListBox = new System.Windows.Forms.ListBox();
            this.scannerDataGridView = new System.Windows.Forms.DataGridView();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INDEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LEXEME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stringDelimiterGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.stringsPanel = new System.Windows.Forms.Panel();
            this.integersPanel = new System.Windows.Forms.Panel();
            this.identifiersPanel = new System.Windows.Forms.Panel();
            this.lexemesPanel = new System.Windows.Forms.Panel();
            this.lexemesGroupBox = new System.Windows.Forms.GroupBox();
            this.keywordsPanel = new System.Windows.Forms.Panel();
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.delimiters1Panel = new System.Windows.Forms.Panel();
            this.delimiters2Panel = new System.Windows.Forms.Panel();
            this.sourceGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.keywordsGroupBox.SuspendLayout();
            this.stringsGroupBox.SuspendLayout();
            this.integersGroupBox.SuspendLayout();
            this.identifiersGroupBox.SuspendLayout();
            this.delimiters1GroupBox.SuspendLayout();
            this.delimiters2GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scannerDataGridView)).BeginInit();
            this.stringDelimiterGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.stringsPanel.SuspendLayout();
            this.integersPanel.SuspendLayout();
            this.identifiersPanel.SuspendLayout();
            this.lexemesPanel.SuspendLayout();
            this.lexemesGroupBox.SuspendLayout();
            this.keywordsPanel.SuspendLayout();
            this.sourcePanel.SuspendLayout();
            this.delimiters1Panel.SuspendLayout();
            this.delimiters2Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.AcceptsReturn = true;
            this.sourceTextBox.AcceptsTab = true;
            this.sourceTextBox.AllowDrop = true;
            this.sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceTextBox.Location = new System.Drawing.Point(5, 18);
            this.sourceTextBox.Multiline = true;
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceTextBox.Size = new System.Drawing.Size(418, 226);
            this.sourceTextBox.TabIndex = 0;
            this.sourceTextBox.Text = resources.GetString("sourceTextBox.Text");
            this.sourceTextBox.TextChanged += new System.EventHandler(this.sourceTextBox_TextChanged);
            // 
            // sourceGroupBox
            // 
            this.sourceGroupBox.Controls.Add(this.sourceTextBox);
            this.sourceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceGroupBox.Location = new System.Drawing.Point(5, 5);
            this.sourceGroupBox.Name = "sourceGroupBox";
            this.sourceGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.sourceGroupBox.Size = new System.Drawing.Size(428, 249);
            this.sourceGroupBox.TabIndex = 0;
            this.sourceGroupBox.TabStop = false;
            this.sourceGroupBox.Text = "Source Code";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 615);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(864, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.compileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(864, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem,
            this.autoScanToolStripMenuItem});
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.compileToolStripMenuItem.Text = "Compile";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.scanToolStripMenuItem.Text = "Run Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.runScanToolStripMenuItem_Click);
            // 
            // autoScanToolStripMenuItem
            // 
            this.autoScanToolStripMenuItem.Checked = true;
            this.autoScanToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScanToolStripMenuItem.Name = "autoScanToolStripMenuItem";
            this.autoScanToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.autoScanToolStripMenuItem.Text = "Auto-Scan";
            this.autoScanToolStripMenuItem.Click += new System.EventHandler(this.autoScanToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "pascal files (*.pas)|*.pas|txt files (*.txt)|*.txt";
            // 
            // keywordsGroupBox
            // 
            this.keywordsGroupBox.Controls.Add(this.keywordsListBox);
            this.keywordsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsGroupBox.Location = new System.Drawing.Point(5, 5);
            this.keywordsGroupBox.Name = "keywordsGroupBox";
            this.keywordsGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.keywordsGroupBox.Size = new System.Drawing.Size(124, 249);
            this.keywordsGroupBox.TabIndex = 0;
            this.keywordsGroupBox.TabStop = false;
            this.keywordsGroupBox.Text = "Keywords";
            // 
            // keywordsListBox
            // 
            this.keywordsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsListBox.FormattingEnabled = true;
            this.keywordsListBox.Items.AddRange(new object[] {
            "procedure",
            "var",
            "Byte",
            "Char",
            "array",
            "of",
            "Longint",
            "String",
            "Begin",
            "if",
            "and",
            "then",
            "else",
            "End"});
            this.keywordsListBox.Location = new System.Drawing.Point(10, 23);
            this.keywordsListBox.Name = "keywordsListBox";
            this.keywordsListBox.Size = new System.Drawing.Size(104, 216);
            this.keywordsListBox.TabIndex = 1;
            // 
            // stringsGroupBox
            // 
            this.stringsGroupBox.Controls.Add(this.stringsListBox);
            this.stringsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringsGroupBox.Location = new System.Drawing.Point(5, 5);
            this.stringsGroupBox.Name = "stringsGroupBox";
            this.stringsGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.stringsGroupBox.Size = new System.Drawing.Size(124, 310);
            this.stringsGroupBox.TabIndex = 0;
            this.stringsGroupBox.TabStop = false;
            this.stringsGroupBox.Text = "Strings";
            // 
            // stringsListBox
            // 
            this.stringsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringsListBox.FormattingEnabled = true;
            this.stringsListBox.Location = new System.Drawing.Point(10, 23);
            this.stringsListBox.Name = "stringsListBox";
            this.stringsListBox.Size = new System.Drawing.Size(104, 277);
            this.stringsListBox.TabIndex = 7;
            // 
            // integersGroupBox
            // 
            this.integersGroupBox.Controls.Add(this.integersListBox);
            this.integersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.integersGroupBox.Location = new System.Drawing.Point(5, 5);
            this.integersGroupBox.Name = "integersGroupBox";
            this.integersGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.integersGroupBox.Size = new System.Drawing.Size(124, 310);
            this.integersGroupBox.TabIndex = 0;
            this.integersGroupBox.TabStop = false;
            this.integersGroupBox.Text = "Integers";
            // 
            // integersListBox
            // 
            this.integersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.integersListBox.FormattingEnabled = true;
            this.integersListBox.Location = new System.Drawing.Point(10, 23);
            this.integersListBox.Name = "integersListBox";
            this.integersListBox.Size = new System.Drawing.Size(104, 277);
            this.integersListBox.TabIndex = 6;
            // 
            // identifiersGroupBox
            // 
            this.identifiersGroupBox.Controls.Add(this.identifiersListBox);
            this.identifiersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifiersGroupBox.Location = new System.Drawing.Point(5, 5);
            this.identifiersGroupBox.Name = "identifiersGroupBox";
            this.identifiersGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.identifiersGroupBox.Size = new System.Drawing.Size(124, 310);
            this.identifiersGroupBox.TabIndex = 0;
            this.identifiersGroupBox.TabStop = false;
            this.identifiersGroupBox.Text = "Identifiers";
            // 
            // identifiersListBox
            // 
            this.identifiersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifiersListBox.FormattingEnabled = true;
            this.identifiersListBox.Location = new System.Drawing.Point(10, 23);
            this.identifiersListBox.Name = "identifiersListBox";
            this.identifiersListBox.Size = new System.Drawing.Size(104, 277);
            this.identifiersListBox.TabIndex = 5;
            // 
            // stringDelimiterTextBox
            // 
            this.stringDelimiterTextBox.AccessibleDescription = "";
            this.stringDelimiterTextBox.AccessibleName = "";
            this.stringDelimiterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringDelimiterTextBox.Location = new System.Drawing.Point(10, 23);
            this.stringDelimiterTextBox.Mask = "&";
            this.stringDelimiterTextBox.Name = "stringDelimiterTextBox";
            this.stringDelimiterTextBox.ReadOnly = true;
            this.stringDelimiterTextBox.Size = new System.Drawing.Size(104, 20);
            this.stringDelimiterTextBox.TabIndex = 4;
            this.stringDelimiterTextBox.Text = "\'";
            this.stringDelimiterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // delimiters1GroupBox
            // 
            this.delimiters1GroupBox.Controls.Add(this.delimiters1ListBox);
            this.delimiters1GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters1GroupBox.Location = new System.Drawing.Point(5, 5);
            this.delimiters1GroupBox.Name = "delimiters1GroupBox";
            this.delimiters1GroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.delimiters1GroupBox.Size = new System.Drawing.Size(124, 194);
            this.delimiters1GroupBox.TabIndex = 0;
            this.delimiters1GroupBox.TabStop = false;
            this.delimiters1GroupBox.Text = "Delimiters 1";
            // 
            // delimiters1ListBox
            // 
            this.delimiters1ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters1ListBox.FormattingEnabled = true;
            this.delimiters1ListBox.Items.AddRange(new object[] {
            ".",
            ";",
            ":",
            "[",
            "]",
            "=",
            "(",
            ")",
            ",",
            "-"});
            this.delimiters1ListBox.Location = new System.Drawing.Point(10, 23);
            this.delimiters1ListBox.Name = "delimiters1ListBox";
            this.delimiters1ListBox.Size = new System.Drawing.Size(104, 161);
            this.delimiters1ListBox.TabIndex = 2;
            // 
            // delimiters2GroupBox
            // 
            this.delimiters2GroupBox.Controls.Add(this.delimiters2ListBox);
            this.delimiters2GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters2GroupBox.Location = new System.Drawing.Point(5, 5);
            this.delimiters2GroupBox.Name = "delimiters2GroupBox";
            this.delimiters2GroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.delimiters2GroupBox.Size = new System.Drawing.Size(124, 249);
            this.delimiters2GroupBox.TabIndex = 0;
            this.delimiters2GroupBox.TabStop = false;
            this.delimiters2GroupBox.Text = "Delimiters 2";
            // 
            // delimiters2ListBox
            // 
            this.delimiters2ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters2ListBox.FormattingEnabled = true;
            this.delimiters2ListBox.Items.AddRange(new object[] {
            "..",
            ":="});
            this.delimiters2ListBox.Location = new System.Drawing.Point(10, 23);
            this.delimiters2ListBox.Name = "delimiters2ListBox";
            this.delimiters2ListBox.Size = new System.Drawing.Size(104, 216);
            this.delimiters2ListBox.TabIndex = 3;
            // 
            // scannerDataGridView
            // 
            this.scannerDataGridView.AllowUserToAddRows = false;
            this.scannerDataGridView.AllowUserToDeleteRows = false;
            this.scannerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scannerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TYPE,
            this.INDEX,
            this.LEXEME});
            this.scannerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannerDataGridView.Location = new System.Drawing.Point(10, 23);
            this.scannerDataGridView.Name = "scannerDataGridView";
            this.scannerDataGridView.ReadOnly = true;
            this.scannerDataGridView.Size = new System.Drawing.Size(408, 277);
            this.scannerDataGridView.TabIndex = 8;
            // 
            // TYPE
            // 
            this.TYPE.HeaderText = "TYPE";
            this.TYPE.Name = "TYPE";
            this.TYPE.ReadOnly = true;
            // 
            // INDEX
            // 
            this.INDEX.HeaderText = "INDEX";
            this.INDEX.Name = "INDEX";
            this.INDEX.ReadOnly = true;
            // 
            // LEXEME
            // 
            this.LEXEME.HeaderText = "LEXEME";
            this.LEXEME.Name = "LEXEME";
            this.LEXEME.ReadOnly = true;
            // 
            // stringDelimiterGroupBox
            // 
            this.stringDelimiterGroupBox.Controls.Add(this.stringDelimiterTextBox);
            this.stringDelimiterGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stringDelimiterGroupBox.Location = new System.Drawing.Point(5, 199);
            this.stringDelimiterGroupBox.Name = "stringDelimiterGroupBox";
            this.stringDelimiterGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.stringDelimiterGroupBox.Size = new System.Drawing.Size(124, 55);
            this.stringDelimiterGroupBox.TabIndex = 9;
            this.stringDelimiterGroupBox.TabStop = false;
            this.stringDelimiterGroupBox.Text = "String Delimiter";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.stringsPanel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.integersPanel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.identifiersPanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lexemesPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.keywordsPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sourcePanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.delimiters1Panel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.delimiters2Panel, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 591);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // stringsPanel
            // 
            this.stringsPanel.Controls.Add(this.stringsGroupBox);
            this.stringsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringsPanel.Location = new System.Drawing.Point(727, 268);
            this.stringsPanel.Name = "stringsPanel";
            this.stringsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.stringsPanel.Size = new System.Drawing.Size(134, 320);
            this.stringsPanel.TabIndex = 11;
            // 
            // integersPanel
            // 
            this.integersPanel.Controls.Add(this.integersGroupBox);
            this.integersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.integersPanel.Location = new System.Drawing.Point(587, 268);
            this.integersPanel.Name = "integersPanel";
            this.integersPanel.Padding = new System.Windows.Forms.Padding(5);
            this.integersPanel.Size = new System.Drawing.Size(134, 320);
            this.integersPanel.TabIndex = 11;
            // 
            // identifiersPanel
            // 
            this.identifiersPanel.Controls.Add(this.identifiersGroupBox);
            this.identifiersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifiersPanel.Location = new System.Drawing.Point(447, 268);
            this.identifiersPanel.Name = "identifiersPanel";
            this.identifiersPanel.Padding = new System.Windows.Forms.Padding(5);
            this.identifiersPanel.Size = new System.Drawing.Size(134, 320);
            this.identifiersPanel.TabIndex = 11;
            // 
            // lexemesPanel
            // 
            this.lexemesPanel.Controls.Add(this.lexemesGroupBox);
            this.lexemesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lexemesPanel.Location = new System.Drawing.Point(3, 268);
            this.lexemesPanel.Name = "lexemesPanel";
            this.lexemesPanel.Padding = new System.Windows.Forms.Padding(5);
            this.lexemesPanel.Size = new System.Drawing.Size(438, 320);
            this.lexemesPanel.TabIndex = 11;
            // 
            // lexemesGroupBox
            // 
            this.lexemesGroupBox.Controls.Add(this.scannerDataGridView);
            this.lexemesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lexemesGroupBox.Location = new System.Drawing.Point(5, 5);
            this.lexemesGroupBox.Name = "lexemesGroupBox";
            this.lexemesGroupBox.Padding = new System.Windows.Forms.Padding(10);
            this.lexemesGroupBox.Size = new System.Drawing.Size(428, 310);
            this.lexemesGroupBox.TabIndex = 11;
            this.lexemesGroupBox.TabStop = false;
            this.lexemesGroupBox.Text = "Lexemes Table";
            // 
            // keywordsPanel
            // 
            this.keywordsPanel.Controls.Add(this.keywordsGroupBox);
            this.keywordsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsPanel.Location = new System.Drawing.Point(447, 3);
            this.keywordsPanel.Name = "keywordsPanel";
            this.keywordsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.keywordsPanel.Size = new System.Drawing.Size(134, 259);
            this.keywordsPanel.TabIndex = 11;
            // 
            // sourcePanel
            // 
            this.sourcePanel.Controls.Add(this.sourceGroupBox);
            this.sourcePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourcePanel.Location = new System.Drawing.Point(3, 3);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Padding = new System.Windows.Forms.Padding(5);
            this.sourcePanel.Size = new System.Drawing.Size(438, 259);
            this.sourcePanel.TabIndex = 11;
            // 
            // delimiters1Panel
            // 
            this.delimiters1Panel.Controls.Add(this.delimiters1GroupBox);
            this.delimiters1Panel.Controls.Add(this.stringDelimiterGroupBox);
            this.delimiters1Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters1Panel.Location = new System.Drawing.Point(587, 3);
            this.delimiters1Panel.Name = "delimiters1Panel";
            this.delimiters1Panel.Padding = new System.Windows.Forms.Padding(5);
            this.delimiters1Panel.Size = new System.Drawing.Size(134, 259);
            this.delimiters1Panel.TabIndex = 11;
            // 
            // delimiters2Panel
            // 
            this.delimiters2Panel.Controls.Add(this.delimiters2GroupBox);
            this.delimiters2Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delimiters2Panel.Location = new System.Drawing.Point(727, 3);
            this.delimiters2Panel.Name = "delimiters2Panel";
            this.delimiters2Panel.Padding = new System.Windows.Forms.Padding(5);
            this.delimiters2Panel.Size = new System.Drawing.Size(134, 259);
            this.delimiters2Panel.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 637);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MinimumSize = new System.Drawing.Size(880, 675);
            this.Name = "MainForm";
            this.Text = "Compiler (by http://github.com/DV1X3R)";
            this.sourceGroupBox.ResumeLayout(false);
            this.sourceGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.keywordsGroupBox.ResumeLayout(false);
            this.stringsGroupBox.ResumeLayout(false);
            this.integersGroupBox.ResumeLayout(false);
            this.identifiersGroupBox.ResumeLayout(false);
            this.delimiters1GroupBox.ResumeLayout(false);
            this.delimiters2GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scannerDataGridView)).EndInit();
            this.stringDelimiterGroupBox.ResumeLayout(false);
            this.stringDelimiterGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.stringsPanel.ResumeLayout(false);
            this.integersPanel.ResumeLayout(false);
            this.identifiersPanel.ResumeLayout(false);
            this.lexemesPanel.ResumeLayout(false);
            this.lexemesGroupBox.ResumeLayout(false);
            this.keywordsPanel.ResumeLayout(false);
            this.sourcePanel.ResumeLayout(false);
            this.delimiters1Panel.ResumeLayout(false);
            this.delimiters2Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.GroupBox sourceGroupBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox keywordsGroupBox;
        private System.Windows.Forms.ListBox keywordsListBox;
        private System.Windows.Forms.GroupBox stringsGroupBox;
        private System.Windows.Forms.ListBox stringsListBox;
        private System.Windows.Forms.GroupBox integersGroupBox;
        private System.Windows.Forms.ListBox integersListBox;
        private System.Windows.Forms.GroupBox identifiersGroupBox;
        private System.Windows.Forms.ListBox identifiersListBox;
        private System.Windows.Forms.MaskedTextBox stringDelimiterTextBox;
        private System.Windows.Forms.GroupBox delimiters1GroupBox;
        private System.Windows.Forms.ListBox delimiters1ListBox;
        private System.Windows.Forms.GroupBox delimiters2GroupBox;
        private System.Windows.Forms.ListBox delimiters2ListBox;
        private System.Windows.Forms.DataGridView scannerDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INDEX;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEXEME;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoScanToolStripMenuItem;
        private System.Windows.Forms.GroupBox stringDelimiterGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox lexemesGroupBox;
        private System.Windows.Forms.Panel stringsPanel;
        private System.Windows.Forms.Panel integersPanel;
        private System.Windows.Forms.Panel identifiersPanel;
        private System.Windows.Forms.Panel lexemesPanel;
        private System.Windows.Forms.Panel keywordsPanel;
        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.Panel delimiters1Panel;
        private System.Windows.Forms.Panel delimiters2Panel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}

