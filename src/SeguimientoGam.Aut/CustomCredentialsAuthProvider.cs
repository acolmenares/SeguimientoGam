using System.Collections.Generic;
using System.Linq;
using SeguimientoGam.InterfacesGestion;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;

namespace SeguimientoGam.Aut
{
	public class CustomCredentialsAuthProvider : CredentialsAuthProvider
	{
		/// <summary>
		/// Paso 2 
		/// </summary>
		/// <param name="authService"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>

		public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
		{
			//Add here your custom auth logic (database calls etc)
			//Return true if credentials are valid, otherwise false
			//authService.

			var usuarios = authService.TryResolve<IUsuarioGestorConsultas>();
			Usuario usuario;
			var aut = usuarios.TryAuthenticate(userName, password, out usuario);
			if (aut)
			{
				var sesion = authService.GetSession();
				sesion.UserAuthId = usuario.PersonaId.ToString();
				sesion.UserName = userName;
			}

			return aut;
		}

		/// <summary>
		/// paso 3
		/// </summary>
		/// <param name="authService"></param>
		/// <param name="session"></param>
		/// <param name="tokens"></param>
		/// <param name="authInfo"></param>
		/// <returns></returns>

		public override IHttpResult OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
		{

			//Fill IAuthSession with data you want to retrieve in the app eg:

			var personas = authService.TryResolve<IPersonaGestorConsultas>();

			var persona = personas.Consultar(int.Parse(session.UserAuthId), session.UserAuthName);
			session.LastName = persona.Apellidos;
			session.FirstName = persona.Nombres;
			session.Email = persona.Correo;
			session.DisplayName = persona.Nombres;
			session.ProfileUrl = persona.Foto;
			session.Roles = persona.Perfiles.Select(x => x.Nombre).ToList();
			session.Permissions = new List<string>();

			var gestorRegionales = authService.TryResolve<IRegionalGestorConsultas>();

			var gestorColaboradores = authService.TryResolve<IColaboradorGestorConsultas>();



			var user = session.ConvertTo<CustomUserSession>();
			user.PersonaConPerfiles = persona;
			user.Regionales = gestorRegionales.Consultar().Results;

			user.Colaboradores = gestorColaboradores.Consultar();

			//Call base method to Save Session and fire Auth/Session callbacks:
			return base.OnAuthenticated(authService, user, tokens, authInfo);

			//Alternatively avoid built-in behavior and explicitly save session with
			//authService.SaveSession(session, SessionExpiry);
			//return null;
		}


		/// <summary>
		/// Paso 1
		/// </summary>
		/// <param name="authService"></param>
		/// <param name="session"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		/// 
		/*public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            AuthenticateResponse authResponse= (AuthenticateResponse) base.Authenticate(authService, session, request);

            return new
            {
                UserName = authResponse.UserName,
                SessionId = authResponse.SessionId,
                ReferrerUrl = authResponse.ReferrerUrl,
                SessionExpires = DateTime.Now,
                Avis = " que bien"
            };
        }
        */

	}
}
