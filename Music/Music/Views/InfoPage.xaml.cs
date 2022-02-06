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
    public partial class InfoPage : ContentPage {
        public IItem Item { get; set; }
        public new string Title { get; set; }
        public InfoPage(IItem item) {
            InitializeComponent();

            this.Item = item;
            Title = item.Name;

            if (item.GetType() == typeof(ArtistItem)) {
                AlbumList.ItemsSource = ((ArtistItem)item).Albums;
            } else if (item.GetType() == typeof(AlbumItem)) {
                AlbumList.ItemsSource = App.GetSongsByAlbum(((AlbumItem)item).Name);
            }

            BindingContext = this;
        }

        private async void AlbumList_ItemTapped(object sender, ItemTappedEventArgs e) {
            try {
                await Navigation.PushAsync(new InfoPage(e.Item as AlbumItem));
            } catch {
                await Navigation.PushModalAsync(new PlayerPage((SongItem)e.Item as SongItem, App.GetSongsByAlbum(((AlbumItem)this.Item).Name).Select(x => x.idPath).ToList()));
            }
        }
    }
}