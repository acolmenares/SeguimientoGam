using SeguimientoGam.Modelos.Autenticacion;
using ServiceStack;
using ServiceStack.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Peticiones;

namespace SeguimientoGam.Aut
{
    public class CustomUserSession : AuthUserSession
    {
        public string CustomId { get; set; }

        public PersonaConPerfiles PersonaConPerfiles {get;set;}

		public List<Colaborador> Colaboradores { get; set; }

		public List<RegionalConMunicipios> Regionales { get; set; }

		public List<string> Generos { get; set; }

		public List<string> TiposDocumento { get; set; }

        public CustomUserSession()
        {
            PersonaConPerfiles = new PersonaConPerfiles();
			Colaboradores = new List<Colaborador>();
			Regionales = new List<RegionalConMunicipios>();

			Generos = new List<string>();

			TiposDocumento = new List<string>();
        }

        /// viene despues de CredentialsAuthProvider
        /*
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            // resto del código....

        }
        */
    }
}
