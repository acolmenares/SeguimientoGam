namespace SeguimientoGam.Modelos.Interfaces
{
	public interface ISaveResponse<T> : IServiceResponse
	{
		T Data { get; set; }
	}
}
