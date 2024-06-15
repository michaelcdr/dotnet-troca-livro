using Microsoft.Extensions.Options;
using System;

namespace TrocaLivro.Aplicacao.Helpers
{
    public static class AppServicesHelper
    {
        static IServiceProvider services = null;
         
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)  throw new Exception("Can't set once a value has already been set.");

                services = value;
            }
        }

        public static AmbienteConfigHelper Config
        {
            get
            {
                var service = services
                    .GetService(typeof(IOptionsMonitor<AmbienteConfigHelper>)) as IOptionsMonitor<AmbienteConfigHelper>;
                AmbienteConfigHelper config = service.CurrentValue;
                return config;
            }
        }
    } 
}
