using System;
using System.Collections.Generic;
using System.Text;

namespace ProLogin
{

    class Producto
    {
        public string Nombre { get; set; }
        public string Url { get; set; }

        public float Cantidad { get; set; }

        public bool Check { get; set; }

        public Producto( string Nombre, float Cantidad, string Url="", bool Check=false) 
        {
            this.Nombre = Nombre; 
            this.Url = Url;
            this.Cantidad = Cantidad;
            this.Check = Check;
        }   
    }
}
