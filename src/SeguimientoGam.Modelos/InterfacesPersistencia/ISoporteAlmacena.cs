using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
    public interface ISoporteAlmacena:  ISoporteConsulta , IAlmacenaEntidad<Soporte>
    {
        Task<CrearResponse<Soporte>> CrearAsync(SoporteCrear modelo);
    }

    public interface ISoporteConsulta
    {
        Task<QueryResponse<Soporte>> ConsultarAsync(SoporteConsultar modelo, 
            Dictionary<string, string> peticion,  
            Expression<Func<Soporte, bool>> filtro = null);

    }
}
