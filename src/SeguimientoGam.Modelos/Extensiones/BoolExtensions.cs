using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.Extensiones
{
    public static class BoolExtensions
    {
        public static void SiEsNullEntonces(this bool? v, Action ejecutarSiEsNull)
        {
            if (!v.HasValue ) ejecutarSiEsNull();
        }
    }
}
