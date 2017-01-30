using System;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Autenticacion
{
	[Alias("usuario_perfil")]
	public class UsuarioPerfil
	{

		[Alias("nombre_usuario")]
		public string NombreUsuario {get;set;}

		[Alias("idperfil")]
		public int PerfilId { get; set; }
	}
}

