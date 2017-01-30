using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("regional")]
    public class Regional : IEntidad
    {
        [Alias("idregional")]
        [AutoIncrement]
        public int Id { get; set; }

        [Alias("nombre_regional")]
        public string Nombre { get; set; }

        [Alias("codigo_regional")]
        public string Codigo { get; set; }

        [Alias("idpais")]
        public int PaisId { get; set; }

        [Alias("iddepto")]
        public int DepartamentoId { get; set; }

        [Alias("idmunicipio")]
        public int MunicipioId { get; set; }

    }
}
