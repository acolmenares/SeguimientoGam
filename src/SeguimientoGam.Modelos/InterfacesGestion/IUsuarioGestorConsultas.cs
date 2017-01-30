using SeguimientoGam.Modelos.Autenticacion;

namespace SeguimientoGam.InterfacesGestion
{
	public interface IUsuarioGestorConsultas
	{
		bool TryAuthenticate(string nombre, string clave, out Usuario usuario);
	}
}

