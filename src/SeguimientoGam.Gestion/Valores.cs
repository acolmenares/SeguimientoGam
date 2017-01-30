using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Gestion
{
    public class Valores
    {
        IAppSettings settings;

        public Valores(IAppSettings settings)
        {

            this.settings = settings;
        }

        public AlivioEmocionalParametros AlivioEmocionalParametros
        {
            get
            {
                return settings.Get("AlivioEmocionalParametros", new AlivioEmocionalParametros());
            }
        }
        

    }
}
