using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;

namespace SeguimientoGam.Gestion
{
    public class Personas:IPersonaGestorConsultas
	{
		readonly IPersonaConsulta repositorio;

		public Personas(IPersonaConsulta  repositorio)
		{
			this.repositorio = repositorio;
		}

        public PersonaConPerfiles Consultar(int idPersona, string nombreUsuario)
        {
            return repositorio.Consultar(idPersona, nombreUsuario);
           
        }
                
    }
}

