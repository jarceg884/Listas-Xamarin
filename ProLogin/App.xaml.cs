﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProLogin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Inicialización de NavigationPage
            MainPage = new NavigationPage(new MainPage());


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
