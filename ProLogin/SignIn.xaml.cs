using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ProLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
        }



        private async void signIn_Clicked(object sender, EventArgs e)
        {
            if (usuario.Text != "" || contraseña.Text != "" || email.Text != "")
            {
                if (IsValidEmail(email.Text))
                {
                    HttpClient cliente = new HttpClient();

                    string ApiUrl = "https://g8a0c0fa384744a-proyectocsharp.adb.us-sanjose-1.oraclecloudapps.com/ords/admin/usuarios/usuarios";

                    Usuario user = new Usuario { email = email.Text, user = usuario.Text , password = contraseña.Text.ToLower() };
                    var json = JsonConvert.SerializeObject(user);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await cliente.PostAsync(ApiUrl, contentJson);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("BIEN!", "Datos agregados!!", "OK");
                        email.Text = "";
                        usuario.Text = "";
                        contraseña.Text = "";
                        Navigation.PopAsync();

                    }
                    else
                    {
                        await DisplayAlert("ERROR", "OCURRIO UN ERROR A LA HORA DE INGRESAR LOS DATOS!!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "Correo Invalido", "OK");
                }


            }
            else
            {
                await DisplayAlert("ERROR", "Valores Vacios", "OK");
            }


        }

        public static bool IsValidEmail(string input)
        {
            // Regular expression pattern for a simple email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Use the Regex.IsMatch method to check if the input matches the pattern
            return Regex.IsMatch(input, pattern);
        }
    }
}