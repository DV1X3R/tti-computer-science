using System;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

using Coursework.Entities;
using Coursework.Utils;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusicDatabaseManager musicDbManager = new MusicDatabaseManager();
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public MainWindow()
            => InitializeComponent();

        /* -- GUI EVENT HANDLERS -- START */

        private void bnExit_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setControls(false);
            setControlsEditable(false);
            setActiveManipulationTab(null);
            await openFile();
        }

        private async void bnOpen_Click(object sender, RoutedEventArgs e)
            => await openFile();

        private async void bnExport_Click(object sender, RoutedEventArgs e)
            => await exportToCsv();

        private async void bnSelect_Click(object sender, RoutedEventArgs e)
            => await selectRecords();

        private async void bnInsert_Click(object sender, RoutedEventArgs e)
            => await insertRecord();

        private void bnEdit_Click(object sender, RoutedEventArgs e)
            => editRecord();

        private async void bnRemove_Click(object sender, RoutedEventArgs e)
            => await removeRecords();

        //private async void placeholder_Click(object sender, RoutedEventArgs e)
        //=> throw new NotImplementedException();

        /* -- EVENT HANDLERS -- END */

        /* -- GUI INTERACTION -- START */

        // Toggle Select / Export buttons
        private void setControls(bool state)
        {
            foreach (var i in new UIElement[] { bnExport, bnSelect, cbFilter })
                i.IsEnabled = state;
        }

        // Toggle DML buttons
        private void setControlsEditable(bool state)
        {
            foreach (var i in new UIElement[] { bnInsert, bnEdit, bnRemove, tcEntities })
                i.IsEnabled = state;
        }

        private void setActiveManipulationTab(string tag)
        {
            setControlsEditable(false); // Disable DML buttons

            foreach (TabItem i in tcEntities.Items)
            {
                if (tag == i.Tag.ToString())
                {
                    i.IsEnabled = true; // Enable tab
                    tcEntities.SelectedItem = i; // Select tab
                    setControlsEditable(true); // Enable DML buttons
                }
                else
                    i.IsEnabled = false; // Disable tab
            }
        }

        private void clearInputBoxes()
        {
            foreach (var i in new TextBox[] {
                tbAlbumId, tbAlbumTitle, tbArtistId, tbArtistName,
                tbCustomerId, tbCustomerFirstName, tbCustomerLastName, tbCustomerCompany, tbCustomerAddress, tbCustomerCity, tbCustomerCountry, tbCustomerPhone, tbCustomerEmail,
                tbGenreId, tbGenreName, tbInvoiceId, tbInvoiceLineId, tbTrackId, tbTrackName, tbTrackMilliseconds, tbTrackBytes, tbTrackUnitPrice
            })
                i.Text = ""; // clear text boxes

            foreach (var i in new ComboBox[] { cbAlbumArtist, cbInvoiceCustomer, cbInvoiceLineInvoice, cbInvoiceLineTrack, cbTrackAlbum, cbTrackGenre })
                i.SelectedIndex = -1; // clear combo boxes

            dpInvoiceDate.Text = "";
        }

        private void setEditMode(bool enter)
        {
            bnInsert.Content = enter ? "Update" : "Insert";
            bnEdit.Content = enter ? "Cancel" : "Edit";
            bnSelect.IsEnabled = !enter;
            bnRemove.IsEnabled = !enter;
            clearInputBoxes();
        }

        /* -- DATABASE STUFF --*/

        private async Task openFile()
        {
            if (semaphore.CurrentCount == 0) return; // return if something is in progress
            var cs = Dialog.SelectSQLiteFile(); // get connection string
            if (cs == null) return; // return if file is not selected
            await semaphore.WaitAsync(); // await for semaphore

            tbStatus.Text = "Working: Opening connection to " + cs + "...";
            dataGrid.ItemsSource = null; // clear datagrid

            try
            { // open connection, database creation may take some time
                // built in EnsureCreatedAsync method is not that async
                await Task.Run(() => musicDbManager.SetSQLiteConnection(cs));
                setControls(true);
                tbStatus.Text = "Ready: Connection opened successfully";
            }
            catch (SqliteException ex)
            { // corrupted file exception handling
                tbStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error");
            }
            finally { semaphore.Release(); } // release semaphore
        }

        private async Task selectRecords()
        {
            if (semaphore.CurrentCount == 0) return; // return if something is in progress
            await semaphore.WaitAsync(); // await for semaphore

            tbStatus.Text = "Working: Selecting items...";
            dataGrid.ItemsSource = null; // clear datagrid
            setActiveManipulationTab(null); // disable manipulation tabs

            var tag = (cbQueries.SelectedItem as FrameworkElement).Tag.ToString(); // tag from ComboBox
            var filter = (bool)cbFilter.IsChecked ? tbFilter.Text : ""; // filter string
            MusicQueryableItem musicQueryableItem; // get source entity by tag
            MusicDatabaseManager.MusicQueriables.TryGetValue(tag, out musicQueryableItem);

            try
            { // select data
                dataGrid.ItemsSource = await Task.Run(() => musicDbManager.SelectData(musicQueryableItem, filter));
                await selectAndFillCbox(musicQueryableItem); // fill combo boxes
                setActiveManipulationTab(tag); // enable manipulation tab
                tbStatus.Text = "Ready: Query executed successfully";
            }
            catch (SqliteException ex)
            { // corrupted file exception handling
                tbStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Error");
            }
            finally { semaphore.Release(); } // release semaphore
        }

        private async Task selectAndFillCbox(MusicQueryableItem musicQueryableItem)
        {
            DataView data;
            EnumerableRowCollection<DataRow> selection;

            switch (musicQueryableItem)
            {
                case MusicQueryableItem.Albums:
                    cbAlbumArtist.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Artists));
                    selection = data.Table.AsEnumerable().OrderBy(x => x.Field<string>("Name"));
                    foreach (var i in selection)
                        cbAlbumArtist.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("ArtistId"),
                            Content = string.Format("{0} [ID: {1}]",
                                i.Field<string>("Name"), (i.Field<string>("ArtistId")))
                        });
                    break;

                case MusicQueryableItem.Invoices:
                    cbInvoiceCustomer.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Customers));
                    selection = data.Table.AsEnumerable().OrderBy(x => x.Field<string>("LastName")).ThenBy(x => x.Field<string>("FirstName"));
                    foreach (var i in selection)
                        cbInvoiceCustomer.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("CustomerId"),
                            Content = string.Format("{0} {1} [ID: {2}]",
                                i.Field<string>("LastName"), i.Field<string>("FirstName"), (i.Field<string>("CustomerId")))
                        });
                    break;

                case MusicQueryableItem.InvoiceLines:
                    cbInvoiceLineInvoice.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Invoices));
                    selection = data.Table.AsEnumerable();
                    foreach (var i in selection)
                        cbInvoiceLineInvoice.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("InvoiceId"),
                            Content = string.Format("[ID: {0}]",
                                i.Field<string>("InvoiceId"))
                        });

                    cbInvoiceLineTrack.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Tracks));
                    selection = data.Table.AsEnumerable().OrderBy(x => x.Field<string>("Name"));
                    foreach (var i in selection)
                        cbInvoiceLineTrack.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("TrackId"),
                            Content = string.Format("{0} [ID: {1}]",
                                i.Field<string>("Name"), i.Field<string>("TrackId"))
                        });
                    break;

                case MusicQueryableItem.Tracks:
                    cbTrackAlbum.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Albums));
                    selection = data.Table.AsEnumerable().OrderBy(x => x.Field<string>("Title"));
                    foreach (var i in selection)
                        cbTrackAlbum.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("AlbumId"),
                            Content = string.Format("{0} [ID: {1}]",
                                i.Field<string>("Title"), i.Field<string>("AlbumId"))
                        });

                    cbTrackGenre.Items.Clear();
                    data = await Task.Run(() => musicDbManager.SelectData(MusicQueryableItem.Genres));
                    selection = data.Table.AsEnumerable().OrderBy(x => x.Field<string>("Name"));
                    foreach (var i in selection)
                        cbTrackGenre.Items.Add(new ComboBoxItem()
                        {
                            ToolTip = i.Field<string>("GenreId"),
                            Content = string.Format("{0} [ID: {1}]",
                                i.Field<string>("Name"), i.Field<string>("GenreId"))
                        });
                    break;

                default:
                    break;
            }
        }

        private async Task removeRecords()
        {
            if (semaphore.CurrentCount == 0) return; // return if something is in progress
            await semaphore.WaitAsync(); // await for semaphore

            if (dataGrid.SelectedItems.Count == 0 ||
                MessageBox.Show(string.Format("{0} {1} will be cascade removed. Continue?",
                    dataGrid.SelectedItems.Count, dataGrid.SelectedItems.Count > 1 ? "rows" : "row"), "Warning",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                semaphore.Release();
                return;
            }

            tbStatus.Text = "Working: Removing items...";

            string tag = (tcEntities.SelectedItem as FrameworkElement).Tag.ToString(); // tag from TabControl

            MusicQueryableItem musicQueryableItem; // get source entity by tag
            MusicDatabaseManager.MusicQueriables.TryGetValue(tag, out musicQueryableItem);

            string primaryKey; // get primary key column name by tag
            MusicDatabaseManager.MusicPrimaryKeys.TryGetValue(tag, out primaryKey);

            var ids = new int[dataGrid.SelectedItems.Count];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = int.Parse((
                    ((DataRowView)dataGrid.SelectedItems[i])[primaryKey]
                    .ToString())
                );

            try
            {
                await Task.Run(() => musicDbManager.RemoveRecords(musicQueryableItem, ids));
                tbStatus.Text = string.Format("Ready: {0} records has been successfully removed", ids.Length);
                MessageBox.Show(string.Format("{0} {1} has been successfully removed",
                    ids.Length, ids.Length > 1 ? "records" : "record"), "Success");
            }
            finally { semaphore.Release(); } // release semaphore

            await selectRecords();
        }

        private async Task insertRecord()
        {
            if (semaphore.CurrentCount == 0) return; // return if something is in progress
            await semaphore.WaitAsync(); // await for semaphore

            bool update = bnInsert.Content.ToString() == "Update"; // update or insert
            string tag = (tcEntities.SelectedItem as FrameworkElement).Tag.ToString(); // tag from TabControl

            MusicQueryableItem musicQueryableItem; // get source entity by tag
            MusicDatabaseManager.MusicQueriables.TryGetValue(tag, out musicQueryableItem);

            object record = null; // new / editable record

            switch (musicQueryableItem)
            {
                case MusicQueryableItem.Albums:
                    record = new Album()
                    {
                        AlbumId = tbAlbumId.Text == "" ? 0 : Convert.ToInt32(tbAlbumId.Text),
                        ArtistId = cbAlbumArtist.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbAlbumArtist.SelectedItem).ToolTip),
                        Title = tbAlbumTitle.Text == "" ? null : tbAlbumTitle.Text
                    };
                    break;

                case MusicQueryableItem.Artists:
                    record = new Artist()
                    {
                        ArtistId = tbArtistId.Text == "" ? 0 : Convert.ToInt32(tbArtistId.Text),
                        Name = tbArtistName.Text == "" ? null : tbArtistName.Text
                    };
                    break;

                case MusicQueryableItem.Customers:
                    record = new Customer()
                    {
                        CustomerId = tbCustomerId.Text == "" ? 0 : Convert.ToInt32(tbCustomerId.Text),
                        FirstName = tbCustomerFirstName.Text == "" ? null : tbCustomerFirstName.Text,
                        LastName = tbCustomerLastName.Text == "" ? null : tbCustomerLastName.Text,
                        Company = tbCustomerCompany.Text == "" ? null : tbCustomerCompany.Text,
                        Address = tbCustomerAddress.Text == "" ? null : tbCustomerAddress.Text,
                        City = tbCustomerCity.Text == "" ? null : tbCustomerCity.Text,
                        Country = tbCustomerCountry.Text == "" ? null : tbCustomerCountry.Text,
                        Phone = tbCustomerPhone.Text == "" ? null : tbCustomerPhone.Text,
                        Email = tbCustomerEmail.Text == "" ? null : tbCustomerEmail.Text
                    };
                    break;

                case MusicQueryableItem.Genres:
                    record = new Genre()
                    {
                        GenreId = tbGenreId.Text == "" ? 0 : Convert.ToInt32(tbGenreId.Text),
                        Name = tbGenreName.Text == "" ? null : tbGenreName.Text
                    };
                    break;

                case MusicQueryableItem.Invoices:
                    record = new Invoice()
                    {
                        InvoiceId = tbInvoiceId.Text == "" ? 0 : Convert.ToInt32(tbInvoiceId.Text),
                        CustomerId = cbInvoiceCustomer.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbInvoiceCustomer.SelectedItem).ToolTip),
                        InvoiceDate = dpInvoiceDate.SelectedDate ?? DateTime.MinValue
                    };
                    break;

                case MusicQueryableItem.InvoiceLines:
                    record = new InvoiceLine()
                    {
                        InvoiceLineId = tbInvoiceLineId.Text == "" ? 0 : Convert.ToInt32(tbInvoiceLineId.Text),
                        InvoiceId = cbInvoiceLineInvoice.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbInvoiceLineInvoice.SelectedItem).ToolTip),
                        TrackId = cbInvoiceLineTrack.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbInvoiceLineTrack.SelectedItem).ToolTip)
                    };
                    break;

                case MusicQueryableItem.Tracks:
                    record = new Track()
                    {
                        TrackId = tbTrackId.Text == "" ? 0 : Convert.ToInt32(tbTrackId.Text),
                        AlbumId = cbTrackAlbum.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbTrackAlbum.SelectedItem).ToolTip),
                        GenreId = cbTrackGenre.SelectedItem == null ? 0 : Convert.ToInt32(((FrameworkElement)cbTrackGenre.SelectedItem).ToolTip),
                        Name = tbTrackName.Text == "" ? null : tbTrackName.Text,
                        Milliseconds = tbTrackMilliseconds.Text == "" ? 0 : Convert.ToInt32(tbTrackMilliseconds.Text),
                        Bytes = tbTrackBytes.Text == "" ? 0 : Convert.ToInt32(tbTrackBytes.Text),
                        UnitPrice = tbTrackUnitPrice.Text == "" ? 0 : Convert.ToInt32(tbTrackUnitPrice.Text)
                    };
                    break;

                default:
                    throw new NotImplementedException();
            }

            try
            {
                if (update) musicDbManager.UpdateRecord(musicQueryableItem, record); // update
                else musicDbManager.InsertRecord(musicQueryableItem, record); // insert

                tbStatus.Text = "Ready: Record has been successfully " + (update ? "updated" : "added");
                MessageBox.Show("Record has been successfully " + (update ? "updated" : "added"), "Success");
                setEditMode(false);
            }
            catch (DbUpdateException ex)
            { // Constraints
                tbStatus.Text = "Error: " + ex.InnerException.Message;
                MessageBox.Show(ex.InnerException.Message, "Error");
            }
            finally { semaphore.Release(); } // release semaphore
        }

        private void editRecord()
        {
            if (bnEdit.Content.ToString() == "Edit")
            {
                var data = (DataRowView)dataGrid.SelectedItem;
                if (data == null) return; // no record selected
                setEditMode(true);

                string tag = (tcEntities.SelectedItem as FrameworkElement).Tag.ToString(); // tag from TabControl

                MusicQueryableItem musicQueryableItem; // get source entity by tag
                MusicDatabaseManager.MusicQueriables.TryGetValue(tag, out musicQueryableItem);

                switch (musicQueryableItem)
                {
                    case MusicQueryableItem.Albums:
                        tbAlbumId.Text = data["AlbumId"].ToString();
                        cbAlbumArtist.SelectedIndex = findByToolTip(cbAlbumArtist, data["ArtistId"].ToString());
                        tbAlbumTitle.Text = data["Title"].ToString();
                        break;

                    case MusicQueryableItem.Artists:
                        tbArtistId.Text = data["ArtistId"].ToString();
                        tbArtistName.Text = data["Name"].ToString();
                        break;

                    case MusicQueryableItem.Customers:
                        tbCustomerId.Text = data["CustomerId"].ToString();
                        tbCustomerFirstName.Text = data["FirstName"].ToString();
                        tbCustomerLastName.Text = data["LastName"].ToString();
                        tbCustomerCompany.Text = data["Company"].ToString();
                        tbCustomerAddress.Text = data["Address"].ToString();
                        tbCustomerCity.Text = data["City"].ToString();
                        tbCustomerCountry.Text = data["Country"].ToString();
                        tbCustomerPhone.Text = data["Phone"].ToString();
                        tbCustomerEmail.Text = data["Email"].ToString();
                        break;

                    case MusicQueryableItem.Genres:
                        tbGenreId.Text = data["GenreId"].ToString();
                        tbGenreName.Text = data["Name"].ToString();
                        break;

                    case MusicQueryableItem.Invoices:
                        tbInvoiceId.Text = data["InvoiceId"].ToString();
                        cbInvoiceCustomer.SelectedIndex = findByToolTip(cbInvoiceCustomer, data["CustomerId"].ToString());
                        dpInvoiceDate.Text = data["InvoiceDate"].ToString();
                        break;

                    case MusicQueryableItem.InvoiceLines:
                        tbInvoiceLineId.Text = data["InvoiceLineId"].ToString();
                        cbInvoiceLineInvoice.SelectedIndex = findByToolTip(cbInvoiceLineInvoice, data["InvoiceId"].ToString());
                        cbInvoiceLineTrack.SelectedIndex = findByToolTip(cbInvoiceLineTrack, data["TrackId"].ToString());
                        break;

                    case MusicQueryableItem.Tracks:
                        tbTrackId.Text = data["TrackId"].ToString();
                        cbTrackAlbum.SelectedIndex = findByToolTip(cbTrackAlbum, data["AlbumId"].ToString());
                        cbTrackGenre.SelectedIndex = findByToolTip(cbTrackGenre, data["GenreId"].ToString());
                        tbTrackName.Text = data["Name"].ToString();
                        tbTrackMilliseconds.Text = data["Milliseconds"].ToString();
                        tbTrackBytes.Text = data["Bytes"].ToString();
                        tbTrackUnitPrice.Text = data["UnitPrice"].ToString();
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
            else // Cancel
                setEditMode(false);
        }

        private int findByToolTip(ComboBox comboBox, string id)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
                if (((ComboBoxItem)comboBox.Items[i]).ToolTip.ToString() == id)
                    return i;
            return -1;
        }

        /* -- GUI INTERACTION -- END */

        /* -- OTHER STUFF -- */

        private async Task exportToCsv()
        {
            if (semaphore.CurrentCount == 0) return; // return if something is in progress
            await semaphore.WaitAsync(); // await for semaphore

            var cs = Dialog.SelectCsvFile(); // get connection string
            if (cs != null)
            {
                dataGrid.SelectAllCells(); // select datagrid data
                dataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader; // include headers when copying
                ApplicationCommands.Copy.Execute(null, dataGrid); // copy datagrid data to buffer
                dataGrid.UnselectAllCells(); // clear selection
                System.IO.StreamWriter sw = new System.IO.StreamWriter(cs, false, System.Text.Encoding.GetEncoding(1251));
                sw.WriteLine((string)Clipboard.GetData(DataFormats.CommaSeparatedValue));
                sw.Close();
                System.Diagnostics.Process.Start(cs); // open file
            }

            semaphore.Release(); // release semaphore
        }

    }
}
