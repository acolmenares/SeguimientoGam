using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IEventoCalendarioGestor : IEventoCalendarioGestorConsultas, IGestor<EventoCalendario>
	{
	}

	public interface IEventoCalendarioGestorConsultas : IGestorConsulta<EventoCalendario>
	{
		Task<QueryResponse<EventoCalendario>> ConsultarAsync(EncuestaGamEventoCalendarioConsultar modelo,
															 IRequest peticion);
	}
}