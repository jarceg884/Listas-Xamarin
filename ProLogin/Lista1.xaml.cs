using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista1 : ContentPage
    {
        ObservableCollection<Producto> productos;
        float progreso = 0;
        public Lista1()
        {
            InitializeComponent();
            productos = new ObservableCollection<Producto>();

            //productos.add(new producto("tomate", "https://s1.eestatic.com/2021/07/12/actualidad/595952167_195030066_1706x960.jpg"));

            //productos.add(new producto("papas", "https://saborusa.com.pa/imagesmg/imagenes/5ff3e6a0b703f_potatoes-food-supermarket-agriculture-jg7qgny.jpg"));

            coleccion.ItemsSource = productos;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            string nombre = await DisplayPromptAsync("Question", "Escribe el nombre del producto", keyboard: Keyboard.Text);

            if (nombre != null && nombre != "")
            {
                if (!productos.Any(obj => obj.Nombre.ToLower() == nombre.ToLower())
)
                {
                    var newProducto = new Producto(nombre, "https://img.freepik.com/premium-vector/paper-bag-with-food-grocery-delivery-concept-vector-illustration-cartoon-style_650087-41.jpg");
                    productos.Add(newProducto);

                    barraProgreso.Progress = progreso / productos.Count;

                }
                else
                {
                    await DisplayAlert("Alerta", "Producto repetido!", "OK");

                }

            }



        }

        private void tachado_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //este monton de codigo es para encontrar el labael que esta a la par del checkbox y cambiarle el atributo de normal a tachado o viceversa
            var checkbox = sender as CheckBox;
            var parentsatackLayout = checkbox.Parent as StackLayout;
            var label = parentsatackLayout.Children[2] as Label;
            bool isChecked = e.Value;
            var item = parentsatackLayout.BindingContext as Producto;


            if (isChecked)
            {
                progreso++;
                barraProgreso.Progress = progreso / productos.Count;
                label.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {

                progreso--;
                barraProgreso.Progress = progreso / productos.Count;
                label.TextDecorations = TextDecorations.None;
            }


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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


    }
}