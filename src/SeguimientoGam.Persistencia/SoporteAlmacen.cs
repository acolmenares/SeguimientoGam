using SeguimientoGam.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System.Linq.Expressions;

namespace SeguimientoGam.Persistencia
{
    public class SoporteAlmacen : Almacen<Soporte>, ISoporteAlmacena
    {
        readonly IAlmacenaEntidad repositorio;

        public SoporteAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<QueryResponse<Soporte>> ConsultarAsync(SoporteConsultar modelo, 
            Dictionary<string, string> peticion, Expression<Func<Soporte, bool>> filtro = null)
        {
             return await base.ConsultarAsync(modelo, peticion, filtro);
            
        }

        public async Task<CrearResponse<Soporte>> CrearAsync(SoporteCrear modelo, EntidadAutoinfo<Soporte> autoinfoAlCrear)
        {
            
            var evento = await repositorio.ConsultarElPrimeroAsync<EventoCalendario>(q => q.Id == modelo.EventoId);

            autoinfoAlCrear.SetValue(v => v.RegionalId, evento.RegionalId);
            autoinfoAlCrear.SetValue(v => v.ProyectoId, evento.ProyectoId);
            autoinfoAlCrear.SetValue(v => v.Extension, MimeTypes.GetMimeType(modelo.Nombre));
            autoinfoAlCrear.SetValue(v => v.Tamanio, modelo.RequestStream.Length);

            repositorio.IniciarTransaccion();

            var soporte = await base.CrearAsync(modelo, autoinfoAlCrear);

            soporte.Data.Nombre = "{0}_{1}".Fmt(soporte.Data.Id, soporte.Data.Nombre);

            soporte.Data.Ruta = "soportes/{0}_{1}/{2}".Fmt(soporte.Data.ProyectoId, soporte.Data.RegionalId, soporte.Data.Nombre);
            
            // guardar el arhivo.... necesito el app config para saber la ruta !!!!

            await repositorio.ActualizarAsync(soporte.Data);

            repositorio.FinalizarTransaccion();

            return soporte;
        }

        
    }
}

