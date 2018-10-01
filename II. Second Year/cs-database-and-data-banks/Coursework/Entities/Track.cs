using System.Collections.Generic;

namespace Coursework.Entities
{
    public class Track
    {
        public int TrackId { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public double UnitPrice { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }
}
