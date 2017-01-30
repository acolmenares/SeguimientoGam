using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace SeguimientoGam.Persistencia
{
	public class GamAlmacen : Almacen<Gam>, IGamAlmacena
    {
		readonly IAlmacenaEntidad repositorio;

        public GamAlmacen(IAlmacenaEntidad repositorio):base(repositorio)
        {
			this.repositorio = repositorio;
		}

		public async Task<QueryResponse<GamConMiembros>> ConsultarAsync(GamConMiembrosConsultar modelo, 
		                                               Dictionary<string, string> peticion, 
		                                               Expression<Func<Gam, bool>> filtro = null)
		{
			var r = await repositorio.ConsultarAsync(modelo, peticion, filtro);
			var miembros = await repositorio.ConsultarAsync<MiembroGam>(
				q => r.Results.Select(x => x.Id).Contains(q.GamId));

			r.Results.ForEach(x =>
			{
				x.Miembros= miembros.FindAll(y => y.GamId == x.Id).ToList();
			});

			return r;
		}


        public async Task<ActualizarResponse<GamConMiembros>> ActualizarAsync(GamActualizar modelo, EntidadAutoinfo<Gam> autoinfoAlActualizar)
        {
            var gam = await base.ActualizarAsync(modelo, autoinfoAlActualizar);
            var conMiembros = new GamConMiembros();

            conMiembros.PopulateWith(gam.Data);
            var gamId = gam.Data.Id;                 
            conMiembros.Miembros= await repositorio.ConsultarAsync<MiembroGam>(q=>q.GamId== gamId);
            

            return new ActualizarResponse<GamConMiembros> { Data = conMiembros };
        }



        protected override  void PreprocesarAlCrear(Gam entidad, EntidadAutoinfo<Gam> autoinfoAlCrear)
        {
            autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
            base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
        }
		           
               
    }
}
