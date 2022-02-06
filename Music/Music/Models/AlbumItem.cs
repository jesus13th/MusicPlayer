using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models {
    public class AlbumItem : IItem {
        public string Name { get; set; }
        public SongItem[] Songs { get; set; }
        public string TotalSongs { 
            get => $"Total Songs: {Songs.Length}"; 
            private set { }
        }
    }
}
