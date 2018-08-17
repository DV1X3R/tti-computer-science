using System.Collections.Generic;

namespace Coursework.Entities
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }
}
