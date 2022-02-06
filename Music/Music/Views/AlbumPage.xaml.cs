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
    public partial class AlbumPage : ContentPage {
        public AlbumPage() {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnAppearing() {
            base.OnAppearing();

            AlbumList.ItemsSource = App.albums;
        }
        private async void AlbumList_ItemTapped(object sender, ItemTappedEventArgs e) {
            AlbumItem item = e.Item as AlbumItem;
            await Navigation.PushAsync(new InfoPage(item));
        }
    }
}