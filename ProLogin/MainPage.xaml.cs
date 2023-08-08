using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using ProLogin;
using System.Xml.Linq;

namespace ProLogin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            loginBtn.Clicked += LoginBtn_Clicked;
            //Pongo esto para hacer el testing de signin mas facil

            
        }

 

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            // Obtener el usuario y la contraseña inssgresados por el usuario
            if ((usuario.Text == "" || contraseña.Text == ""))
            {
                await DisplayAlert("ERROR", "Entradas Vacias!!", "OK");
            }
            else
            {

            
            HttpClient cliente = new HttpClient();
            //este es el link para el el REST

            string apiUrl = "https://g8a0c0fa384744a-proyectocsharp.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/usuarios/verificar";

            Usuario user = new Usuario { user=usuario.Text, password = contraseña.Text };
            var json = JsonConvert.SerializeObject(user);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await cliente.PostAsync(apiUrl, contentJson);


                    if (response.IsSuccessStatusCode)
                    {
                        /*
                         * Usuario: Wilson777
                         * Contraseña: 1234
                         * 
                         * MariRex
                         * 1234
                         * 
                         * SamiBeast
                         * 1234
                         * 
                         * DavidFlo
                         * 1234
                */


                        // iniciar sesión exitosamente
                        await DisplayAlert("Inicio de Sesión", "¡Inicio de Sesión Exitoso!", "Aceptar");

                        //cambio de página a menú en caso de ingreso éxitoso
                        await Navigation.PushAsync(new Menu(usuario.Text));
                    
                    usuario.Text = "";
                    contraseña.Text = "";

                }
                else
                {
                    await DisplayAlert("ERROR", "Credenciales Invalidas!!", "OK");

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"an error occurred: {ex.Message}");
            }
            }
        }

        private async void signIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }
    }
}
