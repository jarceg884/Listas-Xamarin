using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProLogin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Color topBarColor = Color.FromHex("#4daf51");
            //Inicialización de NavigationPage
            MainPage = new NavigationPage(new MainPage())
            {

                BarBackgroundColor = topBarColor,

            };

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
