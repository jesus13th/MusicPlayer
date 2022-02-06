using System;
using Music.Models;
using System.Collections.Generic;

using Music.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Linq;
using TagLib.Ape;
using MediaManager;
using TagLib;
using Xamarin.Essentials;

using static Android.Graphics.Path;

namespace Music {
    public partial class App : Application {

        public static readonly string path = "/storage/emulated/0/Music/";
        public static List<SongItem> songs = new List<SongItem>();
        public static List<ArtistItem> artists = new List<ArtistItem>();
        public static List< AlbumItem> albums = new List<AlbumItem>();

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            
            foreach (var files in Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories)) {
                songs.Add(GetMusicData(files));
            }
            foreach (var artist in songs.Select(x => x.Artist).Distinct()) {
                artists.Add(new ArtistItem() { Name = artist, Albums = GetAlbumsByArtist(artist) });
            }
            foreach (var album in songs.Select(x => x.Album).Distinct()) {
                albums.Add(new AlbumItem() { Name = album });
            }
        }
        private static AlbumItem[] GetAlbumsByArtist(string artist) {
            List<AlbumItem> result = new List<AlbumItem>();

            foreach (var album in songs.Where(x => x.Artist == artist).Select(x => x.Album).Distinct()) {
                result.Add(new AlbumItem() { Name =  album });
            }

            return result.ToArray();
        }
        public static SongItem[] GetSongsByAlbum(string album) { 
            return songs.Where(x => x.Album == album).ToArray();
        }
        public static SongItem GetMusicData(string file) {
            var tFile = TagLib.File.Create(file);

            return new SongItem() { idPath = tFile.Name, Artist = tFile.Tag.FirstAlbumArtist, Album = tFile.Tag.Album, Name = tFile.Tag.Title, Duration = tFile.Properties.Duration, Year = tFile.Tag.Year };
        }
        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
