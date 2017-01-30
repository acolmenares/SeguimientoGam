using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
    
	public interface IMiembroGamAlmacena: IAlmacenaEntidad<MiembroGam>, IMiembroGamConsulta
    {
        
    }

	public interface IMiembroGamConsulta:IConsultaEntidad<MiembroGam>
    {
        
    }
    
}
