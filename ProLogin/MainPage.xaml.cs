using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProLogin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            loginBtn.Clicked += LoginBtn_Clicked;
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            // Obtener el usuario y la contraseña inssgresados por el usuario
            string usuarioIngresado = usuario.Text;
            string contraseñaIngresada = contraseña.Text;

            // Datos de usuario y contraseña almacenados en variables
            string usuarioGuardado = "admin";
            string contraseñaGuardada = "admin";

            // Verificar si el usuario y la contraseña son correctos
            if (usuarioIngresado == usuarioGuardado && contraseñaIngresada == contraseñaGuardada)
            {
                // Iniciar sesión exitosamente
                DisplayAlert("Inicio de sesión", "¡Inicio de sesión exitoso!", "Aceptar");
            }
            else
            {
                // Mostrar mensaje de error si el usuario y/o la contraseña son incorrectos
                DisplayAlert("Inicio de sesión", "Usuario y/o contraseña incorrectos", "Aceptar");
            }
        }
    }
}
