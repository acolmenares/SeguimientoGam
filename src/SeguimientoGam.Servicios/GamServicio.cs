using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Servicios
{

    [Authenticate]
	public class GamServicio : Service
	{
		public IGamGestor Gams { get; set; }

		public QueryResponse<Gam> Get(GamConsultar modelo)
		{
			return Gams.ConsultarAsync(modelo, Request.GetRequestParams()).Result;
		}


		public QueryResponse<GamConMiembros> Get(GamConMiembrosConsultar modelo)
		{
			return Gams.ConsultarAsync(modelo, Request).Result;
		}

		public CrearResponse<Gam> Post(GamCrear modelo)
		{
			return Gams.CrearAsync(modelo, Request).Result;
		}


		public ActualizarResponse<GamConMiembros> Post(GamActualizar modelo)
		{
			return Gams.ActualizarAsync(modelo, Request).Result;
		}



		public BorrarResponse Post(GamBorrar modelo)
		{
			return Gams.BorrarAsync(modelo.Id, Request).Result;
		}


	}

}
