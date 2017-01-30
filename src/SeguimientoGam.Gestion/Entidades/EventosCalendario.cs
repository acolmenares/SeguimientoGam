using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Extensiones;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;
using SeguimientoGam.Aut;

namespace SeguimientoGam.Gestion
{
	public class EventosCalendario : Gestor<EventoCalendario>, IEventoCalendarioGestor
	{
		readonly IEventoCalendarioAlmacena repositorio;

		public EventosCalendario(IEventoCalendarioAlmacena repositorio) : base(repositorio)
		{
			this.repositorio = repositorio;
		}


		public  async  Task<QueryResponse<EventoCalendario>> ConsultarAsync(EncuestaGamEventoCalendarioConsultar modeloConsultar,
		                                                                  IRequest peticion)
		{
			var alivioEmocional = GetValores(peticion).AlivioEmocionalParametros;
			var sesion = peticion.GetUserSession();
            
			modeloConsultar.LineaDeAccionId.SiEsCero_O_NullEntonces( () => modeloConsultar.LineaDeAccionId= alivioEmocional.LineaDeAccionId );
			modeloConsultar.EncargadoId.SiEsCero_O_NullEntonces(() => modeloConsultar.EncargadoId = sesion.GetPersonaId());
			modeloConsultar.ProyectoId.SiEsCero_O_NullEntonces(() => modeloConsultar.ProyectoId = alivioEmocional.ProyectoId);
			//var filtro = await ConstruirFiltro(modeloConsultar, peticion,sesion);
            
			return await repositorio.ConsultarAsync(modeloConsultar, peticion.GetRequestParams());
		}

		async Task<Expression<Func<EventoCalendario, bool>>> ConstruirFiltro(EncuestaGamEventoCalendarioConsultar modelo, IRequest peticion, CustomUserSession sesion)
		{
			IList<int> regionalIds = new List<int>();

			if (modelo.EncargadoId == sesion.GetPersonaId())
			{
				regionalIds = sesion.Regionales.ConvertAll(x => x.Id);
			}
			else {
				var regionales = peticion.TryResolve<IRegionalGestorConsultas>();
				regionalIds= await regionales.ConsultarRegionalIdsAsignadosAsync(modelo.EncargadoId.Value);
			}

			Expression<Func<EventoCalendario, bool>> filtro = entidad => regionalIds.Contains( entidad.RegionalId);
			return filtro;

		}


}
}

