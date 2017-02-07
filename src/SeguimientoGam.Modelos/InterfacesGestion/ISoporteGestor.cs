using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.Web;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
    public interface ISoporteGestor : ISoporteGestorConsultas, IGestor<Soporte>
    {
        Task<CrearResponse<Soporte>> ActualizarAsync(SoporteCrear modelo, IRequest peticion);

    }

    public interface ISoporteGestorConsultas : IGestorConsulta<Soporte>
    {
        Task<QueryResponse<Soporte>> ConsultarAsync(SoporteConsultar modelo, IRequest peticion);
    }
}
