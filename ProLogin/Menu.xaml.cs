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

            Title = "Bienvenido " + username + "!";
        }

        private void OnMenuItemSelected(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.Text)
            {
                case "Productos":
                    Detail = new NavigationPage(new Productos());
                    break;
                case "Lista 1":
                    Detail = new NavigationPage(new Lista1());
                    break;
                case "Lista 2":
                    Detail = new NavigationPage(new Lista2());
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
            }
            IsPresented = false; // cerrar el menú después de seleccionar una opción
        }
    }
}
