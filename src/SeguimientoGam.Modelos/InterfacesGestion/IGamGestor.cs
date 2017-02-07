using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.Web;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
    public interface IGamGestor:IGamGestorConsultas, IGestor<Gam>
	{
        Task<ActualizarResponse<GamConMiembros>> ActualizarAsync(GamActualizar modelo, IRequest peticion);

    }

	public interface IGamGestorConsultas:IGestorConsulta<Gam>
	{
		Task<QueryResponse<GamConMiembros>> ConsultarAsync(GamConMiembrosConsultar modelo, IRequest peticion);
	}
}

