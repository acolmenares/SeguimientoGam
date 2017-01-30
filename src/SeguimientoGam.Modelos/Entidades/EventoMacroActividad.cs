using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("evento_macroactividad")]
    public class EventoMacroActividad
    {
        [Alias("idevento")]
        public int EventoCalendarioId { get; set; } 

        [Alias("idmacroactividad")]
        public int MacroActividadId { get; set; }
    }
}
