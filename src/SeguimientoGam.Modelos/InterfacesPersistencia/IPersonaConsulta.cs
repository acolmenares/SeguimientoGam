using SeguimientoGam.Modelos.Autenticacion;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
    public interface IPersonaConsulta : IConsultaEntidad<Persona>
    {
		PersonaConPerfiles Consultar(int idPersona, string nombreUsuario);
	}
}

