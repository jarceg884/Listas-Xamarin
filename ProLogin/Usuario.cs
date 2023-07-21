using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Shapes;

namespace ProLogin
{
    public class Usuario
    {
        public string username { get; set; }
        public string password { get; set; }
        //public Usuario(string username, string password) 
        //{
        //    this.username = username;
        //    this.password = password;
        //}
    }

    public class ArrayProblem
    {
        public Usuario[] items { get; set; }

    }
}
