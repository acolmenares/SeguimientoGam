using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;
using ServiceStack.Web;
using System.IO;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class SoporteConsultar:QueryDb<Soporte>
    {
    }

    public class SoporteCrear : Crear<Soporte>, IRequiresRequestStream
    {
        public int EventoId { get; set; }
        public string Nombre { get; set; }
        public Stream RequestStream { get; set; }
    }

    public class SoporteBorrar: Borrar<Soporte>
    {

    }

}
