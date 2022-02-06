using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models {
    public class ArtistItem : IItem {
        public string Name { get; set; }
        public AlbumItem[] Albums { get; set; }
    }
}
