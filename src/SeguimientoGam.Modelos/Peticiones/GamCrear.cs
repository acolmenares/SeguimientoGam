using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class  GamCrear:Crear<Gam>
    {
        public int RegionalId { get; set; }
        public int MunicipioId { get; set; }
        public string Nombre { get; set; }
        public int? EncargadoId { get; set; }
        public int? ProyectoId { get; set; }
    }
}
