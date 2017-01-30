using System.Web;

namespace SeguimientoGam.WebHost
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			var appHost = new AppHost();
			appHost.Init();
		}
	}
}
