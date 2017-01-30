using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;


namespace SeguimientoGam.Modelos.Entidades
{

    [Alias("macroactividad")]
    public class MacroActividad:IEntidad
    {
        [AutoIncrement]
        [Alias("idmacroactividad")]
        public int Id { get; set; }

        [Alias("codigo_macroactividad")]
        public decimal Codigo { get; set; }

        [Alias("nombre_macroactividad")]
        public string Nombre { get; set; }

        [Alias("idproyecto")]
        public int ProyectoId { get; set; }

        [Alias("idobjetivo")]
        public int ObjetivoId { get; set; }

        [Alias("idregional")]
        public int RegionalId { get; set; }

        [Alias("idperiodo")]
        public int PeriodoId { get; set; }

        [Alias("idlineaaccion")]
        public int? LineaDeAccionId { get; set; }
    }
}

