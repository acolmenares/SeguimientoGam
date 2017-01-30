using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;

namespace SeguimientoGam.Modelos.Entidades
{
    [Alias("events")]
    public class EventoCalendario:IEntidad
    {
        [AutoIncrement]
        [Alias("id")]
        public int Id { get; set; }

        [Alias("title")]
        public string Title { get; set; }
               

        [Alias("realizacion")]
        public string Realizacion { get; set; }

        [Alias("idproyecto")]
        public int ProyectoId { get; set; }

        [Alias("idpersona")]
        public int EncargadoId { get; set; }

        [Alias("idregional")]
        public int RegionalId { get; set; }

        [Alias("date")]
        public DateTime Fecha { get; set; }


    }
}
