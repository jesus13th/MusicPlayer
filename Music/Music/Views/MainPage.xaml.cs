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
    public partial class MainPage : TabbedPage {
        public MainPage() {
            InitializeComponent();
        }
    }
}   