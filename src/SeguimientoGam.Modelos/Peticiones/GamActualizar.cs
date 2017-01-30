using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.Peticiones
{
	public class GamActualizar : Actualizar<Gam>
	{
		public string Nombre { get; set; }
        public int? EncargadoId { get; set; }
        public int? MunicipioId { get; set; }
    }

}
