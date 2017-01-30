using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;
namespace SeguimientoGam.Modelos.Peticiones
{
	
	public class RespuestaEncuestaGamConsultar : QueryDb<RespuestaEncuestaGam>
	{
	}

	public class RespuestaEncuestaGamCrear : Crear<RespuestaEncuestaGam>
	{
		public int EncuestaGamId { get; set; }
        public int MiembroGamId { get; set; }
        public int PreguntaPlantillaEncuestaId { get; set; }
		public string Valor { get; set; }
        public string Texto { get; set; }

    }

	public class RespuestaEncuestaGamActualizar : Actualizar<RespuestaEncuestaGam>
	{
		public string Valor { get; set; }
        public string Texto { get; set; }
    }

	public class RespuestaEncuestaGamBorrar : Borrar<RespuestaEncuestaGam>
	{
	}
}
