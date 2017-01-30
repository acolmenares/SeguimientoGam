using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IPlantillaEncuestaAlmacena : IAlmacenaEntidad<PlantillaEncuesta>, IPlantillaEncuestaConsulta
	{
	}

	public interface IPlantillaEncuestaConsulta : IConsultaEntidad<PlantillaEncuesta>
	{
	}
}