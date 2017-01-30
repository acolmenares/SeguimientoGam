using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace SeguimientoGam.Persistencia
{
	public class EventoCalendarioAlmacen : Almacen<EventoCalendario>, IEventoCalendarioAlmacena
	{
		readonly IAlmacenaEntidad repositorio;
		public EventoCalendarioAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
			this.repositorio = repositorio;
		}

		public  async Task<QueryResponse<EventoCalendario>> ConsultarAsync(EncuestaGamEventoCalendarioConsultar modelo, 
		                                                             Dictionary<string, string> peticion, 
		                                                             Expression<Func<EventoCalendario, bool>> filtro = null)
		{
			return await repositorio.ConsultarAsync(modelo, peticion, filtro);
		}
	}
}
