using System.Linq;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack;
using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Persistencia
{
    public class PersonaAlmacen : Almacen<Persona>, IPersonaConsulta
    {
		readonly IConsultaEntidad repositorio;
		public PersonaAlmacen(IAlmacenaEntidad repositorio):base(repositorio)
		{
			this.repositorio = repositorio;
		}

		public PersonaConPerfiles Consultar(int idPersona, string nombreUsuario)
		{
            var personaConPerfiles = new PersonaConPerfiles();

			var persona = repositorio.ConsultarPorId<Persona>(idPersona);

			if (persona.Id != 0)
			{
                personaConPerfiles.PopulateWith(persona);

				var usuarioPerfilIds = repositorio
					.Consultar<UsuarioPerfil>(q => q.NombreUsuario==nombreUsuario)
					.Select(x => x.PerfilId).ToList();
				
                personaConPerfiles.Perfiles = repositorio
					.Consultar<Perfil>(q => usuarioPerfilIds.Contains(q.Id));

				var personaRegionalIds = repositorio
					.Consultar<PersonaRegional>(q => q.PersonaId == idPersona)
					.Select(x => x.RegionalId)
					.ToList();
				
                personaConPerfiles.Regionales = repositorio.Consultar<Regional>( 
                    q => personaRegionalIds.Contains(q.Id) 
                    || q.Id==persona.RegionalId
                    );
                                
            }           

			return personaConPerfiles;
		}

	}
}

