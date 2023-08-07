using Xamarin.Forms;
using Xamarin.Essentials;

namespace ProLogin
{
    public partial class Ayuda : ContentPage
    {
        public Ayuda()
        {
            InitializeComponent();
        }

        private async void VerVideoBtn_Clicked(object sender, System.EventArgs e)
        {
            var youtubeUrl = "https://www.youtube.com/watch?v=9sCVcWD1Svs&ab_channel=BetterWalletenEspa%C3%B1ol";
            await Launcher.OpenAsync(youtubeUrl);
        }
    }
}
