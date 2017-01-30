using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack;

namespace SeguimientoGam.Persistencia
{
	public abstract class Almacen<TEntidad>: IConsultaEntidad<TEntidad>, IAlmacenaEntidad<TEntidad> where TEntidad:IEntidad
    {
		readonly IAlmacenaEntidad repositorio;


		protected Almacen(IAlmacenaEntidad respositorio)
        {
            this.repositorio = respositorio;
        }


        public virtual QueryResponse<TEntidad> Consultar(IQueryDb<TEntidad> modelo, 
		                                                 Dictionary<string, string> peticion, 
		                                                 Expression<Func<TEntidad, bool>> filtro = null)
        {
			return repositorio.ConsultarAsync(modelo, peticion,filtro).Result;
        }


        public virtual QueryResponse<Into> Consultar<Into>(IQueryDb<TEntidad, Into> modelo,
		                                                   Dictionary<string,string> peticion, 
		                                                   Expression<Func<TEntidad, bool>> filtro = null)
        {
            return repositorio.ConsultarAsync(modelo, peticion, filtro).Result;
        }



		public virtual List<TEntidad> Consultar(Expression<Func<TEntidad, bool>> predicado)
        {
            return repositorio.ConsultarAsync(predicado).Result;
        }


		public async Task<List<TEntidad>> ConsultarAsync(Expression<Func<TEntidad, bool>> predicado)
        {
            return await repositorio.ConsultarAsync(predicado);
        }


		public async Task<QueryResponse<TEntidad>> ConsultarAsync(IQueryDb<TEntidad> modelo, Dictionary<string, string> peticion, Expression<Func<TEntidad, bool>> filtro = null)
        {
			return await repositorio.ConsultarAsync(modelo, peticion, filtro);
        }


		public async Task<QueryResponse<Into>> ConsultarAsync<Into>(IQueryDb<TEntidad, Into> modelo, Dictionary<string, string> peticion, Expression<Func<TEntidad, bool>> filtro = null)
        {
			return await repositorio.ConsultarAsync(modelo, peticion, filtro);
        }
		                

        public virtual TEntidad ConsultarPorId(TEntidad modelo)
        {
            return ConsultarPorIdAsync(modelo.Id).Result;
        }


        public virtual TEntidad ConsultarPorId(int id)
        {
            return ConsultarPorIdAsync(id).Result;
        }
                        

		public async Task<TEntidad> ConsultarPorIdAsync(TEntidad modelo)
        {
			return await ConsultarPorIdAsync(modelo.Id);
        }

		public async Task<TEntidad> ConsultarPorIdAsync(int id)
        {
			return await repositorio.ConsultarPorIdAsync<TEntidad>(id);
        }

                
        
        public virtual CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo)
        {
            return CrearAsync(modelo, new EntidadAutoinfo<TEntidad>()).Result;
        }


        public virtual CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlCrear)
        {
            return CrearAsync(modelo, autoinfoAlCrear).Result;
        }


		public async  Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo)
		{
			return  await CrearAsync(modelo, new EntidadAutoinfo<TEntidad>());
		}

		public async Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlCrear)
		{
			var entidad = CrearEntidadConDatosDelModelo(modelo);
			PreprocesarAlCrear(entidad, autoinfoAlCrear);
			await repositorio.CrearAsync(entidad);
			return new CrearResponse<TEntidad> { Data = entidad };
		}

        public virtual ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo)
        {
            return ActualizarAsync(modelo, new EntidadAutoinfo<TEntidad>()).Result;
        }


        public virtual ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo, 
            EntidadAutoinfo<TEntidad> autoinfoAlActualizar)
        {
            return ActualizarAsync(modelo, autoinfoAlActualizar).Result;

        }

		public async Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo)
		{
			return await ActualizarAsync(modelo, new EntidadAutoinfo<TEntidad>());

		}

		public async Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlActualizar)
		{
			var entidad = CrearEntidadConDatosDelModelo(modelo);
            PreprocesarAlActualizar(entidad, autoinfoAlActualizar);
            var onlyfields = ObtenerCamposPorActualizar(autoinfoAlActualizar, modelo.GetType());
            await repositorio.ActualizarAsync(entidad, onlyfields);
			entidad = await repositorio.ConsultarPorIdAsync<TEntidad>(entidad.Id);
            return new ActualizarResponse<TEntidad> { Data = entidad };
		}


		public virtual BorrarResponse Borrar(int id)
        {
            return BorrarAsync(id).Result;
        }

		public async Task<BorrarResponse> BorrarAsync(int id)
		{
			await repositorio.BorrarAsync<TEntidad>(id);
			return new BorrarResponse();
		}


		protected virtual void PreprocesarAlCrear(TEntidad entidad, EntidadAutoinfo<TEntidad> autoinfoAlCrear)
        {
            LlenarEntidadConAutoinfo(entidad, autoinfoAlCrear);
        }


		protected virtual void PreprocesarAlActualizar(TEntidad entidad, EntidadAutoinfo<TEntidad> autoinfoAlActualizar)
        {
            LlenarEntidadConAutoinfo(entidad, autoinfoAlActualizar);
        }


        protected  virtual void LlenarEntidadConAutoinfo(TEntidad entidad, EntidadAutoinfo<TEntidad> autoinfo)
        {
            autoinfo.ForEach((k, v) => {
                entidad.GetType().GetProperty(k).SetValue(entidad, v);
            });
        }
		                

        private TEntidad CrearEntidadConDatosDelModelo<TModelo>(TModelo modelo) 
        {
            var entidad = typeof(TEntidad).CreateInstance<TEntidad>();
            entidad.PopulateWith(modelo);
            return entidad;
        }
                

        IList<string> ObtenerCamposPorActualizar(EntidadAutoinfo autoinfo, Type tModelo)
        {
            var nombres = new List<string>();
            var nombresDelModelo = tModelo.GetPropertyNames().Where(x => x != "Id").ToList();
            nombres.AddRange(autoinfo.Keys);
            nombres.AddRange(nombresDelModelo);
            return nombres;
        }

        public async Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado)
        {
            return await repositorio.ConsultarElPrimeroAsync(predicado);
        }

        public T ConsultarElPrimero<T>(Expression<Func<T, bool>> predicado)
        {
            return ConsultarElPrimeroAsync(predicado).Result;
        }
    }
}
