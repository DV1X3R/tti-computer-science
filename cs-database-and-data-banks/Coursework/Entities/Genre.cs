using System.Collections.Generic;

namespace Coursework.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
