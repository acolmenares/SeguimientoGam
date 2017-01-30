using ServiceStack.Model;

namespace SeguimientoGam.Modelos.Interfaces
{
    public interface IEntidad:IHasId<int>
    {
        new int Id { get; set; }
    }
}
