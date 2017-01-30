using ServiceStack;

namespace SeguimientoGam.Modelos.Interfaces
{
	public abstract class Borrar<T> : IReturn<BorrarResponse>
	{
		public int Id {get;set;}
	}

    public class BorrarResponse:ServiceResponse, IBorrarResponse
    {
    }
}
