using System;
using ServiceStack;
using System.Collections.Generic;
using System.Linq.Expressions;
using SeguimientoGam.Modelos.Extensiones;

namespace SeguimientoGam.Modelos.Interfaces
{
	public class EntidadAutoinfo<TEntidad> : EntidadAutoinfo where TEntidad : IEntidad
	{

		public void SetValue<TType>(Expression<Func<TEntidad, TType>> propertyRefExpr, TType value)
		{
			contenedor.SetValue<TEntidad, TType>(propertyRefExpr, value);
		}

		public void ForEach(Action<string, object> p)
		{
			contenedor.ForEach((k, v) => p(k, v));
		}

	}

	public abstract class EntidadAutoinfo
	{

		protected EntidadAutoinfo()
		{
			contenedor = new Dictionary<string, object>();
		}

		public IList<string> Keys
		{
			get { return contenedor.Keys.ToArray(); }
		}


		protected Dictionary<string, object> contenedor;
	}
}
