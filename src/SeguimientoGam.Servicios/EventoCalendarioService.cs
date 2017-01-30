using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class EventoCalendarioServicio : Service
	{
		public IEventoCalendarioGestor Gestor { get; set; }

		public QueryResponse<EventoCalendario> Get(EventoCalendarioConsultar modelo)
		{
			return Gestor.ConsultarAsync(modelo, Request.GetRequestParams()).Result;
		}


		public QueryResponse<EventoCalendario> Get(EncuestaGamEventoCalendarioConsultar modelo)
		{
			return Gestor.ConsultarAsync(modelo, Request).Result;
		}



	}

}
