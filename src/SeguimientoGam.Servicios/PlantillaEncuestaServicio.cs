using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class PlantillaEncuestaServicio : Service
	{
		public IPlantillaEncuestaGestor Gestor { get; set; }

		public QueryResponse<PlantillaEncuesta> Get(PlantillaEncuestaConsultar peticion)
		{
			return Gestor.Consultar(peticion, Request.GetRequestParams());
		}

		public CrearResponse<PlantillaEncuesta> Post(PlantillaEncuestaCrear peticion)
		{
			return Gestor.Crear(peticion, Request);
		}


		public ActualizarResponse<PlantillaEncuesta> Post(PlantillaEncuestaActualizar peticion)
		{
			return Gestor.Actualizar(peticion, Request);
		}


		public BorrarResponse Post(PlantillaEncuestaBorrar peticion)
		{
			return Gestor.Borrar(peticion.Id, Request);
		}

	}
}