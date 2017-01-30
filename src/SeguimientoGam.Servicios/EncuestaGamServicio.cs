using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class EncuestaGamServicio : Service
	{
		public IEncuestaGamGestor Gestor  {get;set;}

		public QueryResponse<EncuestaGamEventoCalendario> Get(EncuestaGamConsultar peticion)
		{
			return Gestor.ConsultarAsync(peticion, Request).Result;
		}


        public QueryResponse<EncuestaGamRespuestas> Get( EncuestaGamRespuestasConsultar peticion)
        {
            return Gestor.ConsultarAsync(peticion, Request.GetRequestParams()).Result;
        }

		public CrearResponse<EncuestaGamEventoCalendario> Post(EncuestaGamCrear peticion)
		{
			return Gestor.CrearAsync(peticion,Request).Result;
		}


		public ActualizarResponse<EncuestaGamEventoCalendario> Post(EncuestaGamActualizar peticion)
		{
			return Gestor.ActualizarAsync(peticion, Request).Result;
		}


        public BorrarResponse Post(EncuestaGamBorrar peticion)
        {
			return Gestor.BorrarAsync(peticion.Id, Request).Result;
        }
        
	}
}
