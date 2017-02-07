using SeguimientoGam.Modelos.Entidades;
using System.IO;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
    public interface IArchivoSoporteAlmacena
    {
        string NombreSoporte(Soporte modelo);

        string RutaSoporte(Soporte modelo);

        Task<long> CrearAsync(Soporte modelo, Stream filestream);
    }
}
