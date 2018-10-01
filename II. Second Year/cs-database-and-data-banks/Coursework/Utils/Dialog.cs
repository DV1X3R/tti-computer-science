using System.Windows;

namespace Coursework.Utils
{
    class Dialog
    {
        public static string SelectSQLiteFile()
            => selectFile(".sqlite", "SQLite database files (*.db *.sqlite *.sqlite3 *.db3)|*.db;*.sqlite;*.sqlite3;*.db3");

        public static string SelectCsvFile()
            => selectFile(".csv", "Comma Separated Values (*.csv)|*.csv");

        private static string selectFile(string defaultExt, string filter)
        {
            // Prepare dialog window
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = defaultExt;
            dlg.Filter = filter;
            dlg.CheckFileExists = false;

            // Show dialog window
            return (dlg.ShowDialog() != true || (
                    !System.IO.File.Exists(dlg.FileName) &&
                    MessageBox.Show("File does not exist, Create?", "New database file", MessageBoxButton.YesNo) == MessageBoxResult.No))
                ? null : dlg.FileName;
        }

    }
}
