using System.Collections.Generic;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
namespace SeguimientoGam.Gestion.Entidades
{
	public class Colaboradores: Gestor<Colaborador>, IColaboradorGestor
	{
		readonly IColaboradorAlmacena repositorio;

		public Colaboradores(IColaboradorAlmacena repositorio) : base(repositorio)
		{
			this.repositorio = repositorio;
		}
		

		public List<Colaborador> Consultar()
		{
			return ConsultarAsync().Result;
		}

		public Task<List<Colaborador>> ConsultarAsync()
		{
			return repositorio.ConsultarAsync(q => true);
		}
	}
}
