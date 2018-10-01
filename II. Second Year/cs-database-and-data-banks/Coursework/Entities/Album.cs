using System.Collections.Generic;

namespace Coursework.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Title { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
