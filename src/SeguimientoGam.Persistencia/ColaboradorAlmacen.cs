using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
namespace SeguimientoGam.Persistencia
{
	public class ColaboradorAlmacen:Almacen<Colaborador>, IColaboradorAlmacena
	{
		public ColaboradorAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
		}

	}
}
