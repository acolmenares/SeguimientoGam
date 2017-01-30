using SeguimientoGam.Modelos.Entidades;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Peticiones;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{

    public interface IEventoCalendarioAlmacena : IAlmacenaEntidad<EventoCalendario>, IEventoCalendarioConsulta
    {
    }

    public interface IEventoCalendarioConsulta : IConsultaEntidad<EventoCalendario>
    {

        Task<QueryResponse<EventoCalendario>> ConsultarAsync(EncuestaGamEventoCalendarioConsultar modelo,
                                                Dictionary<string, string> peticion,
		                                                     Expression<Func<EventoCalendario, bool>> filtro = null);
    }

}
