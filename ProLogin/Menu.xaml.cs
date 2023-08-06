using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class Menu : ContentPage
    {
        public string username;
       
        public Menu(string username)
        {
            InitializeComponent();


   
            //Inicializa boton lista 1
            lista1Btn.Clicked += Lista1Btn_Clicked;
            //Inicializa boton lista 2
            lista2Btn.Clicked += Lista2Btn_Clicked;
            //Inicializa boton productos
            productosBtn.Clicked += ProductosBtn_Clicked;
            //Inicializa boton ubicacion
            ubicacionBtn.Clicked += UbicacionBtn_Clicked;

            Title = "Bienvenido " + username + "!";
        }

        private void UbicacionBtn_Clicked(object sender, EventArgs e)
        {
            //Abre interfaz ubicacion
            ((NavigationPage)this.Parent).PushAsync(new Ubicacion());
        }

        private void Lista2Btn_Clicked(object sender, EventArgs e)
        {
            //Abre interfaz lista 2
            ((NavigationPage)this.Parent).PushAsync(new Lista2());
        }

        private void ProductosBtn_Clicked(object sender, EventArgs e)
        {
            //Abre interfaz productos
            ((NavigationPage)this.Parent).PushAsync(new Productos());
        }

        private void Lista1Btn_Clicked(object sender, EventArgs e)
        {
            //Abre interfaz lista 1
            ((NavigationPage)this.Parent).PushAsync(new Lista1());
        }
    }
}