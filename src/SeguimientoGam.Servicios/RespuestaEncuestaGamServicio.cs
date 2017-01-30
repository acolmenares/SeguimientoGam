using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class RespuestaEncuestaGamServicio : Service
	{
		public IRespuestaEncuestaGamGestor Gestor { get; set; }

		public QueryResponse<RespuestaEncuestaGam> Get(RespuestaEncuestaGamConsultar peticion)
		{
			return Gestor.Consultar(peticion, Request.GetRequestParams());
		}

		public CrearResponse<RespuestaEncuestaGam> Post(RespuestaEncuestaGamCrear peticion)
		{
			return Gestor.Crear(peticion, Request);
		}


		public ActualizarResponse<RespuestaEncuestaGam> Post(RespuestaEncuestaGamActualizar peticion)
		{
			return Gestor.Actualizar(peticion, Request);
		}


		public BorrarResponse Post(RespuestaEncuestaGamBorrar peticion)
		{
			return Gestor.Borrar(peticion.Id, Request);
		}

	}
}
