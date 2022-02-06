using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Music.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongPage : ContentPage {
        public SongPage() {
            InitializeComponent();

            SongList.ItemsSource = App.songs;
        }

        private async void SongList_ItemTapped(object sender, ItemTappedEventArgs e) {
            await Navigation.PushModalAsync(new PlayerPage(e.Item as SongItem, App.songs.Select(x => x.idPath).ToList()));
        }
    }
}