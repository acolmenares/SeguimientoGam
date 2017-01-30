using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IPlantillaEncuestaGestor : IPlantillaEncuestaGestorConsultas, IGestor<PlantillaEncuesta>
	{
	}

	public interface IPlantillaEncuestaGestorConsultas : IGestorConsulta<PlantillaEncuesta>
	{

	}
}