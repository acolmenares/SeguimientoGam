using System.Collections.Generic;
using SeguimientoGam.Modelos.Entidades;
using ServiceStack;

namespace SeguimientoGam.Modelos.Peticiones
{
	public class GamConsultar: QueryDb<Gam>
    {
    }

	public class GamConMiembrosConsultar : QueryDb<Gam, GamConMiembros>
	{
	}

	public class GamConMiembros:Gam
	{
		public GamConMiembros()
		{
			Miembros = new List<MiembroGam>();
		}
		public List<MiembroGam> Miembros { get; set; }

	}
}
