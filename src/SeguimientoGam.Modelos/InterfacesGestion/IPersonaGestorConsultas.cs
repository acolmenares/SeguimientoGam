using SeguimientoGam.Modelos.Autenticacion;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IPersonaGestorConsultas
	{
		PersonaConPerfiles Consultar(int idPersona, string nombreUsuario);
	}
}

