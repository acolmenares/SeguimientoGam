using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IPreguntaPlantillaEncuestaAlmacena : 
	IAlmacenaEntidad<PreguntaPlantillaEncuesta>, IPreguntaPlantillaEncuestaConsulta
	{
	}

	public interface IPreguntaPlantillaEncuestaConsulta : IConsultaEntidad<PreguntaPlantillaEncuesta>
	{
	}
}