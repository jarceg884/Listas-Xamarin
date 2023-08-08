using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Menu : MasterDetailPage
    {
        public string username;

        public Menu(string username)
        {
            InitializeComponent();

            Bienvenido.Text = "Bienvenido " + username + "!";
        }

        private async void OnMenuItemSelected(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.Text)
            {
                case "Productos":
                    Detail = new NavigationPage(new Productos());
                    break;
                case "Lista 1":
                    await Navigation.PushAsync(new Lista1());
                    break;
                case "Ubicación":
                    Detail = new NavigationPage(new Ubicacion());
                    break;
                case "Ayuda":
                    Detail = new NavigationPage(new Ayuda());
                    break;
                case "Contacto":
                    Detail = new NavigationPage(new Contacto());
                    break;
                case "Salir":
                   await Navigation.PopAsync();
                    break;
            }
            IsPresented = false; // cerrar el menú después de seleccionar una opción
        }
    }
}
