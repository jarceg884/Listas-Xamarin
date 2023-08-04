using System;
using System.Collections.Generic;
using System.Text;

namespace ProLogin
{

    class Producto
    {
        public string Nombre { get; set; }
        public string Url { get; set; }

        public bool Check = false;

        public Producto( string Nombre, string Url) 
        {
            this.Nombre = Nombre; 
            this.Url = Url;
        }   
    }
}
