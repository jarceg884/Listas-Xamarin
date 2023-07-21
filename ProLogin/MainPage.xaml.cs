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
        }



        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            // Obtener el usuario y la contraseña inssgresados por el usuario

            HttpClient cliente = new HttpClient();
            //este es el link para el el REST

            string apiUrl = "https://g8a0c0fa384744a-avance2.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/usuarios/usuarios";
            try
            {
                HttpResponseMessage response = await cliente.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string contenido = await response.Content.ReadAsStringAsync();

                    ArrayProblem data = JsonConvert.DeserializeObject<ArrayProblem>(contenido);

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

                    bool entrada = true;
                    foreach (var user in data.items)
                    {
                        if (user.username == usuario.Text && user.password == contraseña.Text)
                        {
                            // iniciar sesión exitosamente
                            await DisplayAlert("Inicio de Sesión", "¡Inicio de Sesión Exitoso!", "Aceptar");

                            //cambio de página a menú en caso de ingreso éxitoso
                            await ((NavigationPage)this.Parent).PushAsync(new Menu());
                            entrada = false;
                        }
                    }
                    if (entrada)
                    {
                        await DisplayAlert("Alerta", "Credenciales Invalidos", "Ok");

                    }
                }
                else
                {
                    Console.WriteLine($"failed to retrieve data. status code: {response.StatusCode}");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"an error occurred: {ex.Message}");
            }
        }


    }
}
