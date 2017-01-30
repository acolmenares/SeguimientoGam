using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IRespuestaEncuestaGamAlmacena : IAlmacenaEntidad<RespuestaEncuestaGam>, 
	IRespuestaEncuestaGamConsulta
	{
	}

	public interface IRespuestaEncuestaGamConsulta : IConsultaEntidad<RespuestaEncuestaGam>
	{
	}
}