using System;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Autenticacion
{
	[Alias("perfil")]
	public class Perfil
	{

		[Alias("idperfil")]
		public int Id { get; set; }

		[Alias("nombre_perfil")]
		public string Nombre { get; set; }

		[Alias("descripcion_perfil")]
		public string Descripcion { get; set; }

		[Alias("icono_perfil")]
		public string Icono { get; set; }


	}
}

