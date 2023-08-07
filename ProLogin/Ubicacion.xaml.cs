using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ProLogin
{
    public partial class Ubicacion : ContentPage
    {
        public Ubicacion()
        {
            InitializeComponent();
        }

        private async void AbrirMapsBtn_Clicked(object sender, EventArgs e)
        {
            var location = new Location(9.9333300, -84.0833300); 
            var options = new MapLaunchOptions { Name = "Mi ubicación" };

            await Map.OpenAsync(location, options);
        }
    }
}
