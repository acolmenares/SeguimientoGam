using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;


namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("lineaaccion")]
    public class LineaDeAccion:IEntidad
    {
        [AutoIncrement]
        [Alias("idlineaaccion")]
        public int Id { get; set; }


        [Alias("codigo_lineaaccion")]
        public decimal Codigo { get; set; }


        [Alias("nombre_lineaaccion")]
        public string Nombre { get; set; }

        [Alias("idobjetivo")]
        public int ObjetivoId { get; set; }

    }


}
