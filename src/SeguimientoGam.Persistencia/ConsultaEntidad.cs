using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Caching;

namespace SeguimientoGam.Persistencia
{

	public class ConsultaEntidad:IConsultaEntidad
    {
		IAutoQueryDb autoQuery;
		private readonly IDbConnection conexion;
		private IDbTransaction transaccion = null;
        protected readonly ICacheClient cacheClient;

        public ConsultaEntidad(IDbConnectionFactory dbConnectionFactory, IAutoQueryDb autoQuery, ICacheClient cacheClient)
        {
            this.cacheClient = cacheClient;

            this.autoQuery = autoQuery;
            try
            {
                conexion = dbConnectionFactory.Open();
            }
            catch (Exception ex){
				Console.WriteLine("Consulta Entidad {0}", ex.Message);
            }
        }

        public T ConsultarPorId<T>(int id)
        {
            return ConsultarPorIdAsync<T>(id).Result;
        }

        public Task<T> ConsultarPorIdAsync<T>(int id)
        {
            return cacheClient.Get<T>(id, () => Execute(async cn =>
            {
                var r = await cn.SingleByIdAsync<T>(id);
                return r ==null ? typeof(T).CreateInstance<T>(): r;
            }));
		}
                

        public T ConsultarPorId<T>(T modelo) where T : IEntidad
        {
            var id = modelo.Id;
            return ConsultarPorId<T>(id);
        }

		public  Task<T> ConsultarPorIdAsync<T>(T modelo) where T : IEntidad
		{
			var id = modelo.Id;
			return  ConsultarPorIdAsync<T>(id);
		}
                

        public QueryResponse<From> Consultar<From>(IQueryDb<From> modelo, 
		                                           Dictionary<string,string> peticion, 
		                                           Expression<Func<From,bool>> filtro= null)
        {
            return ConsultarAsync(modelo, peticion, filtro).Result;
        }

		public  Task<QueryResponse<From>> ConsultarAsync<From>(IQueryDb<From> modelo,
												   Dictionary<string, string> peticion,
												   Expression<Func<From, bool>> filtro = null)
		{
            var q = autoQuery.CreateQuery(modelo, peticion);
            if (filtro != null) q = q.And(filtro);
            var r = autoQuery.Execute(modelo, q);
            return  r.InTask();
		}


        public QueryResponse<Into> Consultar<From, Into>(IQueryDb<From, Into> modelo,
		                                                 Dictionary<string, string> peticion, 
		                                                 Expression<Func<From, bool>> filtro = null)
        {
            var r = ConsultarAsync(modelo, peticion, filtro);
            return r.Result;
        }

		public  Task<QueryResponse<Into>> ConsultarAsync<From, Into>(IQueryDb<From, Into> modelo,
														 Dictionary<string, string> peticion,
														 Expression<Func<From, bool>> filtro = null)
		{
			
            var q = autoQuery.CreateQuery(modelo, peticion);
            if (filtro != null) q = q.And(filtro);
            var r = autoQuery.Execute(modelo, q);
            return r.InTask();
		}

        
        public List<T> Consultar<T>(Expression<Func<T, bool>> predicado)
        {
            return ConsultarAsync(predicado).Result;
        }

		public  Task<List<T>> ConsultarAsync<T>(Expression<Func<T, bool>> predicado)
		{
			return  Execute( async cn => await cn.SelectAsync<T>(predicado));
		}


        public Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado)
        {
            return Execute(async cn =>
            {
                var r = await cn.SingleAsync(predicado);
                return r == null ? typeof(T).CreateInstance<T>() : r;
            });
        }

        public T ConsultarElPrimero<T>(Expression<Func<T, bool>> predicado)
        {
            return ConsultarElPrimeroAsync(predicado).Result;
        }


        public void Dispose()
        {
            Console.WriteLine("ConsultaEntidad: Repositorio Principal Dispose");
            Rollback();
            Execute(con => con.Dispose());
        }


        protected void Execute(Action<IDbConnection> acciones)
        {
            acciones(conexion);
        }

        protected T Execute<T>(Func<IDbConnection, T> acciones)
        {
            return acciones(conexion);
        }


        private void Rollback()
        {
            if (transaccion != null)
            {
                transaccion.Rollback();
                transaccion.Dispose();
                transaccion = null;
            }
        }

        private SqlExpression<From> CrearSqlExpression<From>(Func<From, bool> predicado)
        {
            var qr = CrearSqlExpression<From>();

            return qr;
        }

        private SqlExpression<From> CrearSqlExpression<From>()
        {
            return conexion.From<From>();
        }

        
    }
}
