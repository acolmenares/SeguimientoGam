using ServiceStack;

namespace SeguimientoGam.Modelos.Interfaces
{
	public abstract class Actualizar<T> : IReturn<ActualizarResponse<T>> 
	{ 
		public int Id { get; set; }
	}

	public class ActualizarResponse<T> : ServiceResponse, IActualizarResponse<T>
	{
		public T Data
		{
			get; set;
		}

	}
}
