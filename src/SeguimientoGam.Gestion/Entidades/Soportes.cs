using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;

namespace SeguimientoGam.Gestion.Entidades
{
    public class Soportes : Gestor<Soporte>, ISoporteGestor
    {
        readonly ISoporteAlmacena repositorio;

        public Soportes(ISoporteAlmacena repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<CrearResponse<Soporte>> CrearAsync(SoporteCrear modelo)
        {
            return await repositorio.CrearAsync(modelo);            
        }

        public async Task<QueryResponse<Soporte>> ConsultarAsync(SoporteConsultar modelo, IRequest peticion)
        {
            return await repositorio.ConsultarAsync(modelo, peticion.GetRequestParams(), null);
        }
    }
}
