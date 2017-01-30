using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;

namespace SeguimientoGam.Modelos.Entidades
{
    public class EncuestaGam:IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int GamId { get; set; }
        public int PlantillaEncuestaId { get; set; }
        public int? EventoCalendarioId { get; set; }
        public DateTime FechaRealizacion { get; set;    }
        public short NumeroEncuentro { get; set; }
        public int CreadoPorId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EncargadoId { get; set; }
                
		//public string Reserva { get; set; }
		//public string EstadoReserva { get; set; } // Abierta - Finalizada

		// /encuestagam/reserva/abrir  -  bajar EncuestaGamRepuestas -- no se puede bajar mientras este abierta
		// /encuentagam/reserva/finalizar  -- Subir la respuesta  poner en finalizado


			
    }
}
