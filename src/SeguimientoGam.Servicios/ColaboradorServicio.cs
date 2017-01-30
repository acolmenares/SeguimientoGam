using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class ColaboradorServicio:Service
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public QueryResponse<Colaborador> Get(ColaboradorConsultar modelo)
        {
            var query = AutoQuery.CreateQuery(modelo, Request);
            return AutoQuery.Execute(modelo, query);
        }
    }
}
