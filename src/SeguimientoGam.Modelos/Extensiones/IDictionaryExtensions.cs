using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SeguimientoGam.Modelos.Extensiones
{
	public static class IDictionaryExtensions
	{
		public static void SetValue<TObject,TType>(this IDictionary<string,object> dict, Expression<Func<TObject, TType>> propertyRefExpr, TType value)
		{
			var name =PropertyExtensions.GetName(propertyRefExpr);
			dict[name] = value; 
		}

	}
}

