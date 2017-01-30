using System;

namespace SeguimientoGam.Modelos.Extensiones
{
    public static class IntExtemsions
    {
        public static void SiEsCeroEntonces(this int v, Action ejecutarSiEsCero)
        {
            if (v == 0) ejecutarSiEsCero();
        }

        public static void SiNoEsCeroEntonces(this int v, Action ejecutarSiEsCero)
        {
            if (v == 0) ejecutarSiEsCero();
        }

        public static void SiEsCeroEntonces(this int? v, Action ejecutarSiEsCero)
        {
            if (v.HasValue && v.Value == 0) ejecutarSiEsCero();
        }

        public static void SiEsCero_O_NullEntonces(this int? v, Action ejecutarSiEsCero_O_Null)
        {
            if (!v.HasValue || (v.HasValue && v.Value == 0)) ejecutarSiEsCero_O_Null();
        }

        public static void SiNoEsNullNiCeroEntonces(this int? v, Action ejecutarSiNoEsNullNiCero)
        {
            if (v.HasValue && v.Value != 0) ejecutarSiNoEsNullNiCero();
        }

        public static bool EsCero(this int? v)
        {
            return (v.HasValue && v.Value == 0);
        }

        public static bool NoEsNullNiCero(this int? v)
        {
            return (v.HasValue && v.Value != 0);
        }

        public static bool EsCero_O_Null(this int? v)
        {
            return (!v.HasValue || ( v.HasValue &&  v.Value == 0));
        }

    }

}
