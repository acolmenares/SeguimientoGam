using SeguimientoGam.Modelos.Autenticacion;
using ServiceStack.Auth;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Aut
{
    public static class CustomUserSessionExtensions
    {
        public static bool HasAdminRole(this IAuthSession sesion, IAuthRepository userAuthRepo)
        {                        
            return sesion.HasRole("Admin", userAuthRepo);
        }

        public static bool HasAdminRole(this IAuthSession sesion, IRequest peticion)
        {
            var userAuthRepo = peticion.GetAuthRepo();
            return sesion.HasRole("Admin", userAuthRepo);
        }

        public static int GetPersonaId(this CustomUserSession sesion)
        {
            return sesion.PersonaConPerfiles.Id;
        }


        public static int GetDefaultRegionalId(this CustomUserSession sesion)
        {
            return sesion.PersonaConPerfiles.RegionalId;
        }

        public static bool EsRegionalAutorizada(this CustomUserSession sesion, int regionalId)
        {
            return sesion.PersonaConPerfiles.Regionales.Exists(x => x.Id == regionalId);
        }


    }
}
