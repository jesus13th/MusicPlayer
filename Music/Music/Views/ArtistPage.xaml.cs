using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Music.Models;
using System.IO;

namespace Music.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistPage : ContentPage {

        public ArtistPage() {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            ArtistList.ItemsSource = App.artists;
        }
        private async void ArtistList_ItemTapped(object sender, ItemTappedEventArgs e) {
            ArtistItem item = e.Item as ArtistItem;
            await Navigation.PushAsync(new InfoPage(item));
        }
    }
}