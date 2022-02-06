using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Music.Models;

using MediaManager;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Queue;
using System.IO;

namespace Music.Views {

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage {
        public SongItem Item { get; set; }
        public List<string> QueueSongs { get; set; }

        public PlayerPage(SongItem item, List<string> queueSongs) {
            InitializeComponent();
            this.Item = item;
            this.QueueSongs = queueSongs;
            CrossMediaManager.Current.Init();

            Device.StartTimer(TimeSpan.FromMilliseconds(250), () => {
                Device.BeginInvokeOnMainThread(() => UpdateTime());
                return true;
            });

            BindingContext = this;
        }
        private async void OnMediaItemChanged() {
            await Task.Delay(250);
            titleLabel.Text = CrossMediaManager.Current.Queue.Current.Title;
            ArtistLabel.Text = CrossMediaManager.Current.Queue.Current.Artist;
        }
        protected async override void OnAppearing() {
            base.OnAppearing();
            await CrossMediaManager.Current.Play(QueueSongs);
            await Task.Delay(10);
            await CrossMediaManager.Current.PlayQueueItem(QueueSongs.IndexOf(Item.idPath));

            OnMediaItemChanged();

            playBtn.Source = "pause.png";
            shuffleBtn.Source = CrossMediaManager.Current.ShuffleMode == MediaManager.Queue.ShuffleMode.All ? "shuffle_on.png" : "shuffle_off.png";
            repeatBtn.Source = CrossMediaManager.Current.RepeatMode switch { RepeatMode.Off => "repeat_off.png", RepeatMode.One => "repeat_one.png", RepeatMode.All => "repeat_all.png", _ => throw new NotImplementedException() };
        }
        private void UpdateTime() {
            var position = CrossMediaManager.Current.Position;
            var duration = CrossMediaManager.Current.Duration;
            timePositionLabel.Text = position.ToString(@"mm\:ss");
            timeDurationLabel.Text = duration.ToString(@"mm\:ss");
            timeSlider.Value = position.TotalSeconds / duration.TotalSeconds;
        }
        private async void PlayBtn_Clicked(object sender, EventArgs e) {
            if (CrossMediaManager.Current.IsPlaying()) {
                await CrossMediaManager.Current.Pause();
                playBtn.Source = "play.png";
            } else {
                await CrossMediaManager.Current.PlayPause();
                playBtn.Source = "pause.png";
            }
        }
        private void timeSlider_DragCompleted(object sender, EventArgs e) => CrossMediaManager.Current.SeekTo(TimeSpan.FromTicks((long)(CrossMediaManager.Current.Duration.Ticks * timeSlider.Value)));
        private async void previousBtn_Clicked(object sender, EventArgs e) {
            await CrossMediaManager.Current.PlayPrevious();
            OnMediaItemChanged();
        }
        private async void nextBtn_Clicked(object sender, EventArgs e) {
            await CrossMediaManager.Current.PlayNext();
            OnMediaItemChanged();
        }
        private void shuffleBtn_Clicked(object sender, EventArgs e) {
            CrossMediaManager.Current.ToggleShuffle();
            shuffleBtn.Source = CrossMediaManager.Current.ShuffleMode == MediaManager.Queue.ShuffleMode.All ? "shuffle_on.png" : "shuffle_off.png";
        }
        private void repeatBtn_Clicked(object sender, EventArgs e) {
            CrossMediaManager.Current.ToggleRepeat();
            repeatBtn.Source = CrossMediaManager.Current.RepeatMode switch { RepeatMode.Off => "repeat_off.png", RepeatMode.One => "repeat_one.png", RepeatMode.All => "repeat_all.png", _ => throw new NotImplementedException() };
        }
    }
}