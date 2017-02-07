using System.Collections.Generic;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Caching;

namespace SeguimientoGam.Persistencia
{
    public class AlmacenaEntidad : ConsultaEntidad, IAlmacenaEntidad
    {
        //private readonly ICacheClient cacheClient;

        public AlmacenaEntidad(IDbConnectionFactory dbConnectionFactory, IAutoQueryDb autoQuery, ICacheClient cacheClient) : base(dbConnectionFactory, autoQuery, cacheClient)
        {
        }

        public int Actualizar<T>(T modelo) where T : IEntidad
        {
              return ActualizarAsync(modelo).Result;
        }


		public  Task<int> ActualizarAsync<T>(T modelo) where T : IEntidad
		{
			
            cacheClient.Remove<T>(modelo);
            var result=  Execute( async cn =>
			{                
				var id = modelo.Id;
				var u= await cn.UpdateAsync<T>(modelo, q => q.Id == id);
                var modeloActualizado = await cn.SingleByIdAsync<T>(modelo.Id);
                cacheClient.Set(modeloActualizado);
                return u;
			});
			return  result;
		}


        public int Actualizar<T>(T modelo, IList<string> updateonly) where T : IEntidad
        {
            return ActualizarAsync(modelo, updateonly).Result;
        }


        public Task<int> ActualizarAsync<T>(T modelo, IList<string> updateonly) where T : IEntidad
		{
			cacheClient.Remove(modelo);
			var result = Execute( async cn =>
			{
				var id = modelo.Id;
                var r=  await  cn.UpdateOnlyAsync<T>(modelo, onlyFields: updateonly.ToArray(), where: q => q.Id == id);
                var modeloActualizado = await cn.SingleByIdAsync<T>(modelo.Id);
                cacheClient.Set(modeloActualizado);
                return r;
			});
			return  result;
		}


        public int Borrar<T>(int id) where T : IEntidad
		{
            return BorrarAsync<T>(id).Result;
		}


		public Task<int> BorrarAsync<T>(int id) where T : IEntidad
		{

            cacheClient.Remove<T>(id);
            var result =  Execute( async cn =>
			{
				return await cn.DeleteByIdAsync<T>(id);
			});
			return result;
		}

		public int Borrar<T>(T modelo) where T : class, IEntidad
        {
            var id = modelo.Id;
            return Borrar<T>(id);
        }

		public Task<int> BorrarAsync<T>(T modelo) where T : class, IEntidad
		{
			var id = modelo.Id;
			return BorrarAsync<T>(id);
		}



		public long Crear<T>(T modelo) where T : IEntidad
        {
            return CrearAsync<T>(modelo).Result;
        }


		public  Task<long> CrearAsync<T>(T modelo) where T : IEntidad
		{	
            var result = Execute( async cn => await cn.InsertAsync(modelo, true));

            modelo.Id = (int)result.Result;
            cacheClient.Set(modelo);
            return result;
		}

    }
}
