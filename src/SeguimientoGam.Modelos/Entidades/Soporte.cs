using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("soporte")]
    public class Soporte: IEntidad
    {
        [AutoIncrement]
        [Alias("idsoporte")]
        public int Id { get; set; }
        [Alias("nombre_soporte")]
        public string Nombre { get; set; }
        [Alias("tamano_soporte")]
        public decimal Tamanio { get; set; }
        [Alias("extension_soporte")]
        public string Extension { get; set; }
        [Alias("ruta_soporte")]
        public string Ruta { get; set; }
        [Alias("estado")]
        public int Estado { get; set; }
        [Alias("idevento")]
        public int EventoId { get; set; }
        [Alias("idproyecto")]
        public int ProyectoId { get; set; }
        [Alias("idregional")]
        public int RegionalId { get; set; }
        [Alias("idperiodo")]
        public int PeriodoId { get; set; }
        
    }
}
