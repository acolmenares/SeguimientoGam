using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class PreguntaPlantillaEncuestaServicio : Service
	{
		public IPreguntaPlantillaEncuestaGestor Gestor { get; set; }

		public QueryResponse<PreguntaPlantillaEncuesta> Get(PreguntaPlantillaEncuestaConsultar peticion)
		{
			return Gestor.Consultar(peticion, Request.GetRequestParams());
		}

		public CrearResponse<PreguntaPlantillaEncuesta> Post(PreguntaPlantillaEncuestaCrear peticion)
		{
			return Gestor.Crear(peticion, Request);
		}


		public ActualizarResponse<PreguntaPlantillaEncuesta> Post(PreguntaPlantillaEncuestaActualizar peticion)
		{
			return Gestor.Actualizar(peticion, Request);
		}


		public BorrarResponse Post(PreguntaPlantillaEncuestaBorrar peticion)
		{
			return Gestor.Borrar(peticion.Id, Request);
		}

	}
}
