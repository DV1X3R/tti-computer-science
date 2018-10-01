using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Coursework.Entities;
using Coursework.Utils;

namespace Coursework
{
    sealed class MusicDatabaseManager
    {
        private string connectionString;

        public void SetSQLiteConnection(string filePath)
        {
            connectionString = "Filename=" + filePath;
            using (var db = new MusicDatabaseContext(connectionString))
                db.Database.EnsureCreated();
        }

        public DataView SelectData(MusicQueryableItem musicQueryableItem, string filter = "")
        {
            using (var db = new MusicDatabaseContext(connectionString))
            {
                filter = filter.ToLower();
                switch (musicQueryableItem)
                {
                    case MusicQueryableItem.Albums:
                        return DataConversion.ToDataView((
                                from n in db.albums
                                where n.Title.ToLower().Contains(filter) || n.Artist.Name.ToLower().Contains(filter)
                                select new { n.AlbumId, n.ArtistId, n.Title, Artist = n.Artist.Name }
                            ).ToList());

                    case MusicQueryableItem.Artists:
                        return DataConversion.ToDataView((
                                from n in db.artists
                                where n.Name.ToLower().Contains(filter)
                                select new { n.ArtistId, n.Name }
                            ).ToList());

                    case MusicQueryableItem.Customers:
                        return DataConversion.ToDataView((
                                from n in db.customers
                                where n.FirstName.ToLower().Contains(filter) || n.LastName.ToLower().Contains(filter)
                                    || n.Phone.Contains(filter) || n.Email.ToLower().Contains(filter)
                                select new { n.CustomerId, n.FirstName, n.LastName, n.Company, n.Address, n.City, n.Country, n.Phone, n.Email }
                            ).ToList());

                    case MusicQueryableItem.Genres:
                        return DataConversion.ToDataView((
                                from n in db.genres
                                where n.Name.ToLower().Contains(filter)
                                select new { n.GenreId, n.Name }
                            ).ToList());

                    case MusicQueryableItem.Invoices:
                        return DataConversion.ToDataView((
                            from n in db.invoices
                            where n.Customer.FirstName.ToLower().Contains(filter) || n.Customer.LastName.ToLower().Contains(filter)
                                    || n.Customer.Phone.Contains(filter) || n.Customer.Email.ToLower().Contains(filter)
                            //|| n.InvoiceDate.ToString().Contains(filter)
                            select new { n.InvoiceId, n.CustomerId, n.InvoiceDate, Customer = n.Customer.FirstName + " " + n.Customer.LastName, n.Customer.Phone, n.Customer.Email }
                        ).ToList());

                    case MusicQueryableItem.InvoiceLines:
                        return DataConversion.ToDataView((
                                from n in db.invoice_lines
                                where n.Invoice.Customer.FirstName.ToLower().Contains(filter) || n.Invoice.Customer.LastName.ToLower().Contains(filter)
                                    || n.Invoice.Customer.Phone.Contains(filter) || n.Invoice.Customer.Email.ToLower().Contains(filter)
                                    //|| n.Invoice.InvoiceDate.ToShortDateString().Contains(filter)
                                    || n.Track.Name.ToLower().Contains(filter) || n.Track.Genre.Name.ToLower().Contains(filter)
                                    || n.Track.Album.Title.ToLower().Contains(filter) || n.Track.Album.Artist.Name.ToLower().Contains(filter)
                                select new
                                {
                                    n.InvoiceLineId,
                                    n.InvoiceId,
                                    n.TrackId,
                                    n.Invoice.InvoiceDate,
                                    Customer = n.Invoice.Customer.FirstName + " " + n.Invoice.Customer.LastName,
                                    n.Invoice.Customer.Phone,
                                    n.Invoice.Customer.Email,
                                    Track = n.Track.Name,
                                    Album = n.Track.Album.Title,
                                    Artist = n.Track.Album.Artist.Name,
                                    Genre = n.Track.Genre.Name,
                                    n.Track.Bytes,
                                    n.Track.UnitPrice
                                }
                            ).ToList());

                    case MusicQueryableItem.Tracks:
                        return DataConversion.ToDataView((
                            from n in db.tracks
                            where n.Name.ToLower().Contains(filter) || n.Genre.Name.ToLower().Contains(filter)
                                || n.Album.Title.Contains(filter) || n.Album.Artist.Name.ToLower().Contains(filter)
                            select new { n.TrackId, n.AlbumId, n.GenreId, n.Name, Album = n.Album.Title, Artist = n.Album.Artist.Name, Genre = n.Genre.Name, n.Milliseconds, n.Bytes, n.UnitPrice }
                        ).ToList());

                    case MusicQueryableItem.InvoicesSummary:
                        return DataConversion.ToDataView((
                            from il in db.invoice_lines
                            group il by new
                            {
                                il.Invoice.InvoiceId,
                                il.Invoice.InvoiceDate,
                                il.Invoice.Customer.FirstName,
                                il.Invoice.Customer.LastName
                            }
                            into s
                            select new
                            {
                                s.Key.InvoiceId,
                                s.Key.InvoiceDate,
                                Customer = s.Key.FirstName + " " + s.Key.LastName,
                                TracksQuantity = s.Count(),
                                MbTotal = Math.Round(s.Sum(sg => sg.Track.Bytes / Math.Pow(1024, 2)), 2),
                                PriceTotal = s.Sum(sg => sg.Track.UnitPrice)
                            }
                    ).ToList());

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void RemoveRecords(MusicQueryableItem musicEditableItem, int[] ids)
        {
            using (var db = new MusicDatabaseContext(connectionString))
            {
                switch (musicEditableItem)
                {
                    case MusicQueryableItem.Albums:
                        var albums = new Album[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            albums[i] = new Album() { AlbumId = ids[i] };
                        db.albums.RemoveRange(albums);
                        break;

                    case MusicQueryableItem.Artists:
                        var artists = new Artist[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            artists[i] = new Artist() { ArtistId = ids[i] };
                        db.artists.RemoveRange(artists);
                        break;

                    case MusicQueryableItem.Customers:
                        var customers = new Customer[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            customers[i] = new Customer() { CustomerId = ids[i] };
                        db.customers.RemoveRange(customers);
                        break;

                    case MusicQueryableItem.Genres:
                        var genres = new Genre[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            genres[i] = new Genre() { GenreId = ids[i] };
                        db.genres.RemoveRange(genres);
                        break;

                    case MusicQueryableItem.Invoices:
                        var invoices = new Invoice[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            invoices[i] = new Invoice() { InvoiceId = ids[i] };
                        db.invoices.RemoveRange(invoices);
                        break;

                    case MusicQueryableItem.InvoiceLines:
                        var invoice_lines = new InvoiceLine[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            invoice_lines[i] = new InvoiceLine() { InvoiceLineId = ids[i] };
                        db.invoice_lines.RemoveRange(invoice_lines);
                        break;

                    case MusicQueryableItem.Tracks:
                        var tracks = new Track[ids.Length];
                        for (int i = 0; i < ids.Length; i++)
                            tracks[i] = new Track() { TrackId = ids[i] };
                        db.tracks.RemoveRange(tracks);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                db.SaveChanges();
            }
        }

        public void InsertRecord(MusicQueryableItem musicEditableItem, object obj)
        {
            using (var db = new MusicDatabaseContext(connectionString))
            {
                switch (musicEditableItem)
                {
                    case MusicQueryableItem.Albums: db.albums.Add((Album)obj); break;
                    case MusicQueryableItem.Artists: db.artists.Add((Artist)obj); break;
                    case MusicQueryableItem.Customers: db.customers.Add((Customer)obj); break;
                    case MusicQueryableItem.Genres: db.genres.Add((Genre)obj); break;
                    case MusicQueryableItem.Invoices: db.invoices.Add((Invoice)obj); break;
                    case MusicQueryableItem.InvoiceLines: db.invoice_lines.Add((InvoiceLine)obj); break;
                    case MusicQueryableItem.Tracks: db.tracks.Add((Track)obj); break;

                    default:
                        throw new NotImplementedException();
                }

                db.SaveChanges();
            }
        }

        public void UpdateRecord(MusicQueryableItem musicEditableItem, object obj)
        {
            using (var db = new MusicDatabaseContext(connectionString))
            {
                switch (musicEditableItem)
                {
                    case MusicQueryableItem.Albums: db.albums.Update((Album)obj); break;
                    case MusicQueryableItem.Artists: db.artists.Update((Artist)obj); break;
                    case MusicQueryableItem.Customers: db.customers.Update((Customer)obj); break;
                    case MusicQueryableItem.Genres: db.genres.Update((Genre)obj); break;
                    case MusicQueryableItem.Invoices: db.invoices.Update((Invoice)obj); break;
                    case MusicQueryableItem.InvoiceLines: db.invoice_lines.Update((InvoiceLine)obj); break;
                    case MusicQueryableItem.Tracks: db.tracks.Update((Track)obj); break;

                    default:
                        throw new NotImplementedException();
                }

                db.SaveChanges();
            }
        }

        public static Dictionary<string, string> MusicPrimaryKeys = new Dictionary<string, string>() {
            { "albums", "AlbumId" },
            { "artists", "ArtistId" },
            { "customers", "CustomerId" },
            { "genres", "GenreId" },
            { "invoices", "InvoiceId" },
            { "invoice_lines", "InvoiceLineId" },
            { "tracks", "TrackId" }
        };

        public static Dictionary<string, MusicQueryableItem> MusicQueriables = new Dictionary<string, MusicQueryableItem>() {
            { "albums", MusicQueryableItem.Albums },
            { "artists", MusicQueryableItem.Artists },
            { "customers", MusicQueryableItem.Customers },
            { "genres", MusicQueryableItem.Genres },
            { "invoices", MusicQueryableItem.Invoices },
            { "invoice_lines", MusicQueryableItem.InvoiceLines },
            { "tracks", MusicQueryableItem.Tracks },
            { "invoices_summary", MusicQueryableItem.InvoicesSummary }
        };

    }

    public enum MusicQueryableItem
    {
        Albums,
        Artists,
        Customers,
        Genres,
        Invoices,
        InvoiceLines,
        Tracks,
        InvoicesSummary
    }

}
