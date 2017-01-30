using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.Extensiones
{
    public static class StringExtensions
    {

        public static void SiEstaVacia_O_NullEntonces(this string v, Action ejecutarSiEstavacia_O_Null)
        {
            if ( v.IsNullOrEmpty() ) ejecutarSiEstavacia_O_Null();
        }
    }
}

