using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class MiembroGamServicio : Service
    {
        public IMiembroGamGestor Gestor { get; set; }

        public QueryResponse<MiembroGam> Get(MiembroGamConsultar peticion)
        {
			return Gestor.Consultar(peticion, Request.GetRequestParams());
        }

        public CrearResponse<MiembroGam> Post(MiembroGamCrear peticion)
        {
            return Gestor.Crear(peticion, Request);
        }

        public ActualizarResponse<MiembroGam> Post(MiembroGamActualizar peticion)
        {
            return Gestor.Actualizar(peticion, Request);
        }

        public BorrarResponse Post(MiembroGamBorrar peticion)
        {
            return Gestor.Borrar(peticion.Id, Request);
        }
    }
}