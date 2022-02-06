using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Models {
    public class SongItem : IItem {
        public string idPath { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public uint Year { get; set; }
        public TimeSpan Duration { get; set; }

        public override string ToString() => $"{Name}\n{Album}\n{Artist}\n{Year}\n{Duration}.";
    }
}
