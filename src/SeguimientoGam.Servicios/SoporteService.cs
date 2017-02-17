using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Servicios
{

    [Authenticate]
    public class SoporteService:Service
    {

        public ISoporteGestor Gestor { get; set; }

        public CrearResponse<Soporte> Post(SoporteCrear peticion)
        {
             var r=  Gestor.CrearAsync(peticion).Result;
             return r ; // OJO vaadin-upload : arroja error cuando No es JSON 
            
        }

        public QueryResponse<Soporte> Get(SoporteConsultar peticion)
        {
            return Gestor.ConsultarAsync(peticion, Request).Result;
        }

        public BorrarResponse Post(SoporteBorrar peticion)
        {
            return Gestor.BorrarAsync(peticion, Request).Result;
        }

    }

    
}
