using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeguimientoGam.Gestion
{
    public abstract  class GestorConsulta<TEntidad> : IGestorConsulta<TEntidad> where TEntidad : IEntidad
    {
        public Valores Valores { get; set; }

        protected Func<Valores> GetValoresFn (IRequest request)
        {
            Valores valores = null;
            Func<Valores> func = () => {
                if (valores == null) {
                    valores =request.TryResolve<Valores>();
                }
                return valores;

            };
            return func;
        }

        protected Valores GetValores(IRequest request)
        {
                      
            return request.TryResolve<Valores>();
        }
              

        readonly IConsultaEntidad<TEntidad> repositorio;

		protected GestorConsulta(IConsultaEntidad<TEntidad> repositorio)
        {
            this.repositorio = repositorio;
        }

        public virtual QueryResponse<TEntidad> Consultar(IQueryDb<TEntidad> modelo,
		                                                  Dictionary<string,string> dynamicParams,
		                                                  Expression<Func<TEntidad, bool>> filtro = null)
		{
			return ConsultarAsync(modelo, dynamicParams, filtro).Result;
        }


        public virtual List<TEntidad> Consultar(Expression<Func<TEntidad, bool>> predicado)
        {
			return ConsultarAsync(predicado).Result;
        }

        public virtual TEntidad ConsultarPorId(int id)
        {
			return ConsultarPorIdAsync(id).Result;
        }

        public virtual TEntidad ConsultarPorId(TEntidad modelo)
        {
			return ConsultarPorIdAsync(modelo.Id).Result;
        }

		public async virtual Task<List<TEntidad>> ConsultarAsync(Expression<Func<TEntidad, bool>> predicado)
		{
			return await repositorio.ConsultarAsync(predicado);
		}

		public async virtual Task<QueryResponse<TEntidad>> ConsultarAsync(IQueryDb<TEntidad> modelo, 
		                                                    Dictionary<string, string> peticion, 
		                                                    Expression<Func<TEntidad, bool>> filtro = null)
		{
			return await repositorio.ConsultarAsync(modelo, peticion, filtro);
		}

		public async Task<TEntidad> ConsultarPorIdAsync(TEntidad modelo)
		{
			return await ConsultarPorIdAsync(modelo.Id);
		}

		public async Task<TEntidad> ConsultarPorIdAsync(int id)
		{
			return await repositorio.ConsultarPorIdAsync(id);
		}

        public Func<TEntidad> GetEntityById(int entidadId)
        {
            TEntidad entidadAlmacenada = default(TEntidad);

            Func<TEntidad> func = () =>
            {
                if (entidadAlmacenada == null)
                {
                    entidadAlmacenada =  repositorio.ConsultarPorId(entidadId);
                }
                return entidadAlmacenada;
            };

            return func;
        }

        
        public TEntidad ConsultarElPrimero(Expression<Func<TEntidad, bool>> predicado)
        {
            return ConsultarElPrimeroAsync(predicado).Result;
        }

        public async Task<TEntidad> ConsultarElPrimeroAsync(Expression<Func<TEntidad, bool>> predicado)
        {
            return await repositorio.ConsultarElPrimeroAsync(predicado);
        }
    }

    public abstract class Gestor<TEntidad> : GestorConsulta<TEntidad>, IGestor<TEntidad>, IGestorConsulta<TEntidad> where TEntidad:IEntidad
    {

		readonly IAlmacenaEntidad<TEntidad> repositorio;

        protected Gestor(IAlmacenaEntidad<TEntidad> repositorio):base(repositorio)
        {
            this.repositorio = repositorio;
        }

		public virtual CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo, IRequest peticion) 
        {
			return CrearAsync(modelo, peticion).Result;
        }

		public async virtual Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo, IRequest peticion)
		{
			var autoinfoAlCrear = new EntidadAutoinfo<TEntidad>();
			PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
			return await repositorio.CrearAsync(modelo, autoinfoAlCrear);
		}

        public  virtual ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo, IRequest peticion)  
        {
            
			return ActualizarAsync(modelo, peticion).Result;
        }

		public async virtual Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo, IRequest peticion)

		{
			var autoinfoAlActualizar = new EntidadAutoinfo<TEntidad>();
			PreprocesarAlActualizar(modelo, autoinfoAlActualizar,peticion);
			return await repositorio.ActualizarAsync(modelo, autoinfoAlActualizar);
		}

        public virtual BorrarResponse Borrar(int id, IRequest peticion)   
        {
			return BorrarAsync(id, peticion).Result;
        }

		public async virtual Task<BorrarResponse> BorrarAsync(int id, IRequest peticion)
		{
			return await repositorio.BorrarAsync(id);
		}



		protected virtual void PreprocesarAlCrear(Crear<TEntidad> modelo, 
            EntidadAutoinfo<TEntidad> autoinfoAlCrear,
            IRequest peticion)
            
        {   
        }


		protected virtual void PreprocesarAlActualizar(Actualizar<TEntidad> modelo,
            EntidadAutoinfo<TEntidad> autoinfoAlActualizar, IRequest peticion)
            
        {
		}

       

    }
}
