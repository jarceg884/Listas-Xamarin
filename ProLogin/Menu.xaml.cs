using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using System.Linq;

using static ProLogin.Lista1;
using static ProLogin.Lista1.Item;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Menu : MasterDetailPage
    {
        ObservableCollection<Lista> listas = new ObservableCollection<Lista>();
        public string apiUrl = "https://g8a0c0fa384744a-proyectocsharp.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/lists/mostrar_listas_usuario?usuario=";

        public string fullApi;
        public string username { get; set; }

        HttpClient clienteHTTP = new HttpClient();

        public Menu(string username)
        {
            this.username = username;
            fullApi = apiUrl+username;
            InitializeComponent();

            Bienvenido.Text = "Bienvenido " + username + "!";

            llenarLista(fullApi);

            coleccion.ItemsSource = listas;
        }

        public class Lista
        {
            public string LIST_NAME { get; set; }

            public  Lista(string nombre_lista)
            {
                this.LIST_NAME = nombre_lista;
            }
            public class ArrayProblem
            {
                public Lista[] items { get; set; }

            }
        }

        private async void llenarLista(string link)
        {


            string contenido = await clienteHTTP.GetStringAsync(link);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayProblem>(contenido);

            Console.WriteLine(data);
            foreach (var item in data.items)
            {
                listas.Add(new Lista(item.LIST_NAME ));
            }

        }

        private async void OnMenuItemSelected(object sender, EventArgs e)
        {
            

            var button = (Xamarin.Forms.Button)sender;
            switch (button.Text)
            {
     
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

        private async void lista1Btn_Clicked(object sender, EventArgs e)
        {
            var button = (Xamarin.Forms.Button)sender;
            await Navigation.PushAsync(new Lista1(username, button.Text));
        }


        private async void agregarLista_Clicked(object sender, EventArgs e)
        {
            string nombre = await DisplayPromptAsync("Lista", "Escriba el nombre de la lista ", keyboard: Keyboard.Text);

            if (nombre != null && nombre != "")
            {
                if (!listas.Any(obj => obj.LIST_NAME.ToLower() == nombre.ToLower())
)
                {
                    var newProducto = new Lista(nombre);
                    listas.Add(newProducto);

                }
                else
                {
                    await DisplayAlert("Alerta", "Lista repetido!", "OK");

                }

            }

        }
    }
}
