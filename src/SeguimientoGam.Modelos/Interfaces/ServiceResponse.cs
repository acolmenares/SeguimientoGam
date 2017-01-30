using ServiceStack;
using System.Collections.Generic;

namespace SeguimientoGam.Modelos.Interfaces
{
	public abstract class ServiceResponse : IServiceResponse
	{
		public Dictionary<string, string> Meta
		{
			get; set;
		}

		public ResponseStatus ResponseStatus
		{
			get; set;
		}
	}
}
