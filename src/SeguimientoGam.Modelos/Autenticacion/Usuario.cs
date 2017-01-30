using System;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Autenticacion
{
	[Alias("usuario")]
	public class Usuario
	{
		[Alias("nombre_usuario")]
		public string Nombre { get; set; }
		[Alias("clave_usuario")]
		public string Clave { get; set; }
		[Alias("idpersona")]
		public int PersonaId { get; set; }

	}
}

