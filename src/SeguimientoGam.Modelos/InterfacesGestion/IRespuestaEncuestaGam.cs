
using SeguimientoGam.Modelos.Entidades;


namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IRespuestaEncuestaGamGestor : IRespuestaEncuestaGamGestorConsultas,
	IGestor<RespuestaEncuestaGam>
	{
	}

	public interface IRespuestaEncuestaGamGestorConsultas : IGestorConsulta<RespuestaEncuestaGam>
	{

	}
}