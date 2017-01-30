using SeguimientoGam.Modelos.Autenticacion;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using System.Collections.Generic;

namespace SeguimientoGam.Aut
{
    public static class IRequestExtensions
    {
        public static List<int> GetRegionalesIdAutorizadas(this IRequest peticion)
        {
            return peticion.GetPersonaConPerfiles().Regionales.ConvertAll(q => q.Id);
        }

        public static CustomUserSession GetUserSession(this IRequest peticion)
        {
            return peticion.SessionAs<CustomUserSession>();
        }
    
        public static PersonaConPerfiles GetPersonaConPerfiles(this IRequest peticion)
        {
            var sesion = peticion.SessionAs<CustomUserSession>();
            return sesion.PersonaConPerfiles;
        }

		public static bool EsRegionalAutorizada(this IRequest peticion, int regionalId) {

			return peticion.GetPersonaConPerfiles().Regionales.Exists(x => x.Id == regionalId);
		}

		public static int GetPersonaId(this IRequest peticion)
		{
			return peticion.GetPersonaConPerfiles().Id;
		}


        public static int GetDefaultRegionalId(this IRequest peticion)
        {
            return peticion.GetPersonaConPerfiles().RegionalId;
        }

        public static bool HasAdminRole(this IRequest peticion) {
			var sesion = peticion.GetSession();
			var userAuthRepo = peticion.TryResolve<IAuthRepository>();
			return sesion.HasRole("Admin", userAuthRepo);
		}

        public static IAuthRepository GetAuthRepo(this IRequest peticion)
        {
            return peticion.TryResolve<IAuthRepository>();
        }

    }
}
