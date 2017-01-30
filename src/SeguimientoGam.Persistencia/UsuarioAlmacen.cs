using System.Linq;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesPersistencia;

namespace SeguimientoGam.Persistencia
{
	public class UsuarioAlmacen:  IUsuarioConsulta
	{
		readonly IConsultaEntidad repositorio;
		public UsuarioAlmacen(IAlmacenaEntidad repositorio)
		{
			this.repositorio = repositorio;
		}

		public Usuario Consultar(string nombre)
		{
			return repositorio
				.Consultar<Usuario>(q => q.Nombre == nombre)
				.FirstOrDefault()??new Usuario(); 
		}
	}
}

