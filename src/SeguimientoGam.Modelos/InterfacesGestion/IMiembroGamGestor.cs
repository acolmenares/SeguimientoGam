using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
    public interface IMiembroGamGestor:IMiembroGamGestorConsultas, IGestor<MiembroGam>
    {
        
    }

    public interface IMiembroGamGestorConsultas:IGestorConsulta<MiembroGam>
    {
      
    }
}
