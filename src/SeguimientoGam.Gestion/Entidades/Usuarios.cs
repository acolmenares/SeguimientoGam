using SeguimientoGam.InterfacesGestion;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;

namespace SeguimientoGam.Gestion
{
	public class Usuarios : IUsuarioGestorConsultas
	{
		readonly IUsuarioConsulta repositorio;
		readonly IProveedorHash proveedorHash;

		public Usuarios(IUsuarioConsulta repositorio, IProveedorHash proveedorHash)
		{
			this.repositorio = repositorio;
			this.proveedorHash = proveedorHash;
		}


		public bool TryAuthenticate(string nombre, string clave, out Usuario usuario)
		{
			usuario =repositorio.Consultar(nombre);
			return (usuario != default(Usuario)
			        && usuario.PersonaId!=0
					&& proveedorHash.VerificarHash(clave, usuario.Clave));

		}
	}
}

