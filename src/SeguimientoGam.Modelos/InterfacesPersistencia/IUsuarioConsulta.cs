using SeguimientoGam.Modelos.Autenticacion;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IUsuarioConsulta
	{
		Usuario Consultar(string nombre);
	}
}

