using SeguimientoGam.Modelos.Entidades;
using System;
using System.Collections.Generic;
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
        readonly IArchivoSoporteAlmacena archivoSoporteAlmacen;

        public SoporteAlmacen(IAlmacenaEntidad repositorio, IArchivoSoporteAlmacena archivoSoporteAlmacen) : base(repositorio)
        {
            this.repositorio = repositorio;
            this.archivoSoporteAlmacen = archivoSoporteAlmacen;
        }

        public async Task<QueryResponse<Soporte>> ConsultarAsync(SoporteConsultar modelo, 
            Dictionary<string, string> peticion, Expression<Func<Soporte, bool>> filtro = null)
        {
             return await base.ConsultarAsync(modelo, peticion, filtro);
            
        }

        public async Task<CrearResponse<Soporte>> CrearAsync(SoporteCrear modelo )
        {
                        
            var evento = await repositorio.ConsultarElPrimeroAsync<EventoCalendario>(q => q.Id == modelo.EventoId);

            var autoinfoAlCrear = new EntidadAutoinfo<Soporte>();

            autoinfoAlCrear.SetValue(v => v.RegionalId, evento.RegionalId);
            autoinfoAlCrear.SetValue(v => v.ProyectoId, evento.ProyectoId);
            autoinfoAlCrear.SetValue(v => v.Nombre, modelo.Nombre);
            autoinfoAlCrear.SetValue(v => v.Tamanio, 0);
            autoinfoAlCrear.SetValue(v => v.Ruta, "");
            autoinfoAlCrear.SetValue(v => v.Extension, "");


            repositorio.IniciarTransaccion();

            var soporte = await base.CrearAsync(modelo, autoinfoAlCrear);
            var tamanio = await archivoSoporteAlmacen.CrearAsync(soporte.Data, modelo.RequestStream);
            
            soporte.Data.Nombre = archivoSoporteAlmacen.NombreSoporte(soporte.Data);
            soporte.Data.Ruta = archivoSoporteAlmacen.RutaSoporte(soporte.Data);
            soporte.Data.Extension = MimeTypes.GetMimeType(modelo.Nombre);
            soporte.Data.Tamanio = tamanio;

            await repositorio.ActualizarAsync(soporte.Data);
            
            repositorio.FinalizarTransaccion();

            return soporte;
        }

        
    }
}

