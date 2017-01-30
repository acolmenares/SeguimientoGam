using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;

namespace SeguimientoGam.Modelos.Peticiones
{
	
	public class PreguntaPlantillaEncuestaConsultar : QueryDb<PreguntaPlantillaEncuesta>
	{
	}

	public class PreguntaPlantillaEncuestaCrear : Crear<PreguntaPlantillaEncuesta>
	{
		public string Texto { get; set; }
		public string TipoRespuesta { get; set; }
	}

	public class PreguntaPlantillaEncuestaActualizar : Actualizar<PreguntaPlantillaEncuesta>
	{
		public string Texto { get; set; }
		public string TipoRespuesta { get; set; }
	}

	public class PreguntaPlantillaEncuestaBorrar : Borrar<PreguntaPlantillaEncuesta>
	{

	}

}
