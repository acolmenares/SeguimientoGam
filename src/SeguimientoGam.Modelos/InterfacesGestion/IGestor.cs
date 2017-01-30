using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.Web;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IGestor<TEntidad> : IGestorConsulta<TEntidad> where TEntidad : IEntidad
	{
		CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo, IRequest peticion);
		Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo, IRequest peticion);

		ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo, IRequest peticion);
		Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo, IRequest peticion);

		BorrarResponse Borrar(int id, IRequest peticion);
		Task<BorrarResponse> BorrarAsync(int id, IRequest peticion);
	}
}