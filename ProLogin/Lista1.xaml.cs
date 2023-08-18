using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ProLogin.Lista1.Item;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista1 : ContentPage
    {
        ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
        float progreso = 0;

        public bool chequeable = true;
        string usuario { get; set; }
        string lista_nombre { get; set; }

        HttpClient clienteHTTP = new HttpClient();

        public string fullUrl;

        //esto es la url para hacer un get
        string apiUrl = "https://g8a0c0fa384744a-proyectocsharp.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/lists/lists";
        public Lista1(string usuario, string lista_nombre)
        {

            InitializeComponent();
            this.usuario = usuario;
            this.lista_nombre = lista_nombre;

            nombreUsuario.Text = usuario;
            
           
            string queryString = $"?usuario={usuario}&lista_nombre={lista_nombre}";

            //Aqui unimos el query con el urlGet
            fullUrl = apiUrl + queryString;

            Console.WriteLine(fullUrl);

            //productos.Add(new Producto("papas", 3, "https://saborusa.com.pa/imagesmg/imagenes/5ff3e6a0b703f_potatoes-food-supermarket-agriculture-jg7qgny.jpg"));

            llenarLista(fullUrl);

            coleccion.ItemsSource = productos;
        
        }

        public class Item
        {
            public string PRODUCT_NAME { get; set; }
            public float PRODUCT_QUANTITY { get; set; }
            public int CHECKED { get; set; }
            public string USERNAME { get; set; }
            public string LIST_NAME { get; set; }
            public class ArrayProblem
            {
                public Item[] items { get; set; }

            }
        }
        private async void llenarLista(string link)
        {

          
            string contenido = await clienteHTTP.GetStringAsync(link);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayProblem>(contenido);

            Console.WriteLine(data);
            foreach (var item in data.items)
            {
                productos.Add(new Producto(item.PRODUCT_NAME, item.PRODUCT_QUANTITY, "https://img.freepik.com/premium-vector/paper-bag-with-food-grocery-delivery-concept-vector-illustration-cartoon-style_650087-41.jpg", estaCheck(item.CHECKED)));
            }

        }

        private bool estaCheck(int num)
        {
            if (num == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            string nombre = await DisplayPromptAsync("Escriba", "Escribe el nombre del producto", keyboard: Keyboard.Text);

            string cantidad = await DisplayPromptAsync("Escriba", "Escribe la cantidad", keyboard: Keyboard.Numeric);

            if (nombre != null && nombre != "")
            {
                if (!productos.Any(obj => obj.Nombre.ToLower() == nombre.ToLower())
)
                {
                    var newProducto = new Producto(nombre, float.Parse(cantidad) , "https://img.freepik.com/premium-vector/paper-bag-with-food-grocery-delivery-concept-vector-illustration-cartoon-style_650087-41.jpg");
                    productos.Add(newProducto);

                    //[INSERT AL DB TABLA LISTS]
                    Item productoNuevo = new Item{
                        PRODUCT_NAME = nombre,
                        PRODUCT_QUANTITY = float.Parse(cantidad),
                        CHECKED = 0,
                        USERNAME = usuario,
                        LIST_NAME = lista_nombre

                    };

                    var json = JsonConvert.SerializeObject(productoNuevo);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await clienteHTTP.PostAsync(apiUrl, contentJson);


                    barraProgreso.Progress = progreso / productos.Count;

                }
                else
                {
                    await DisplayAlert("Alerta", "Producto repetido!", "OK");

                }

            }



        }

        private async void tachado_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                if (chequeable)
                {
                    //este monton de codigo es para encontrar el labael que esta a la par del checkbox y cambiarle el atributo de normal a tachado o viceversa
                    var checkbox = sender as CheckBox;
                    var parentsatackLayout = checkbox.Parent as StackLayout;
                    var label = parentsatackLayout.Children[2] as Label;
                    bool isChecked = e.Value;
                    var item = parentsatackLayout.BindingContext as Producto;

                    int chequeado;

                    if (isChecked)
                    {
                        progreso++;
                        barraProgreso.Progress = progreso / productos.Count;
                        label.TextDecorations = TextDecorations.Strikethrough;
                        chequeado = 1;

                    }
                    else
                    {
                        progreso--;
                        barraProgreso.Progress = progreso / productos.Count;
                        label.TextDecorations = TextDecorations.None;
                        chequeado = 0;

                    }

                    if (!base.OnBackButtonPressed())
                    {
                        Item chequeoProducto = new Item
                        {
                            CHECKED = chequeado,
                            LIST_NAME = lista_nombre,
                            USERNAME = usuario,
                            PRODUCT_NAME = label.Text,
                        };
                        var json = JsonConvert.SerializeObject(chequeoProducto);
                        var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await clienteHTTP.PutAsync(apiUrl, contentJson);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //este montonde codigo y de var es para encontrar el checkbox
            var tappedItem = (sender as Image)?.BindingContext as Producto;
            var tappedImage = (sender as Image);
            var parentGrid = tappedImage?.Parent.Parent as Grid;
            var parentStackLayout = parentGrid?.Children[0] as StackLayout;
            var checkBox = parentStackLayout.Children[0] as CheckBox;

            //aqui evalua si el checkbox es true o false 
            if (tappedItem != null)
            {
                barraProgreso.Progress = progreso / productos.Count;
                productos.Remove(tappedItem);

                //ESTE URL ES NUEVO NO ES IGUAL QUE EL DE ARRIBA, ESTE SIRVE PARA BORRAR PRODUCTOS DE LA BASE DE DATOS
                string url = "https://g8a0c0fa384744a-proyectocsharp.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/lists/Borrar_Cosas";
                Item borrarProducto = new Item
                {
                    LIST_NAME = lista_nombre,
                    USERNAME = usuario,
                    PRODUCT_NAME = tappedItem.Nombre
                };
                var json = JsonConvert.SerializeObject(borrarProducto);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await clienteHTTP.PutAsync(url, contentJson);

            }
        }

        private void Enviar_Clicked(object sender, EventArgs e)
        {
            foreach (var item in productos)
            {
                Console.WriteLine(item.Nombre.ToString());
            }
            Console.WriteLine(progreso);
        }

        private async void HandleBackButtonPressed(object sender, EventArgs e)
        {
            // Display an alert when the back button is pressed
            bool result = await DisplayAlert("Confirmation", "Do you want to go back?", "Yes", "No");

            if (result)
            {
                // Handle the back button press (e.g., navigate back)
                await Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Display an alert when the back button is pressed
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await DisplayAlert("Confirmacion", "Seguro que quieres ir atras?", "Si", "No");
                if (result)
                {
                    chequeable = false;
                    await Navigation.PopAsync();
                }
            });

            return true;
        }

        private async void ToolbarItem_Back_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Confirmacion", "Seguro que quieres ir atras?", "Si", "No");

            if (result)
            {
                chequeable = false;

                await Navigation.PopAsync();
            }
        }

    }


}