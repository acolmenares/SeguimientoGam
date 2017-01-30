using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IPreguntaPlantillaEncuestaGestor : IPreguntaPlantillaEncuestaGestorConsultas, IGestor<PreguntaPlantillaEncuesta>
	{
	}

	public interface IPreguntaPlantillaEncuestaGestorConsultas : IGestorConsulta<PreguntaPlantillaEncuesta>
	{

	}
}