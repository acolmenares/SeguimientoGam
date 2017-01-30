
using SeguimientoGam.Modelos.Autenticacion;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	
	public interface IColaboradorAlmacena : IColaboradorConsulta, IAlmacenaEntidad<Colaborador>
	{

	}

	public interface IColaboradorConsulta : IConsultaEntidad<Colaborador>
	{


	}
}
