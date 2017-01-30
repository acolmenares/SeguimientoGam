using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SeguimientoGam.Modelos.Extensiones
{
	public static class PropertyExtensions
	{

		public static void SetPropertyValue<TObject>(this TObject obj, Expression<Func<TObject, object>> propertyRefExpr, object value)
		{
			var propertyName = GetName(propertyRefExpr);
			obj.GetType().GetProperty(propertyName).SetValue(obj, value);
		}

		public static void SetPropertyValue<TObject>(this TObject obj, string propertyName, object value)
		{
			obj.GetType().GetProperty(propertyName).SetValue(obj, value);
		}


		public static object GetPropertyValue<TObject>(this TObject obj, Expression<Func<TObject, object>> propertyRefExpr)
		{
			var propertyName = GetName(propertyRefExpr);
			return GetPropertyValue(obj, propertyName);
		}

		public static object GetPropertyValue<TObject>(this TObject obj, string propertyName)
		{
			return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
		}

		public static string GetPropertyName<TObject>(this TObject type,
													   Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		public static string GetName<TObject, TTipoDato>(Expression<Func<TObject, TTipoDato>> propertyRefExpr)
		{
			return GetPropertyNameCore(propertyRefExpr.Body);
		}

		private static string GetPropertyNameCore(Expression propertyRefExpr)
		{
			if (propertyRefExpr == null)
				throw new ArgumentNullException(nameof(propertyRefExpr), "propertyRefExpr is null.");

			MemberExpression memberExpr = propertyRefExpr as MemberExpression;
			if (memberExpr == null)
			{
				UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
				if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
					memberExpr = unaryExpr.Operand as MemberExpression;
			}

			if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
				return memberExpr.Member.Name;

			throw new ArgumentException("No property reference expression was found.", nameof(propertyRefExpr));
		}
	}


}

