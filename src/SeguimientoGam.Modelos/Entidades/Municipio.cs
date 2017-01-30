using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("municipio")]
    public class Municipio : IEntidad
    {
        [Alias("idmunicipio")]
        public int Id { get; set; }

        [Alias("codigo_municipio")]
        public string Codigo { get; set; }

        [Alias("nombre_municipio")]
        public string Nombre { get; set; }

        [Alias("iddepto")]
        public int DepartamentoId { get; set; }

    }
}
