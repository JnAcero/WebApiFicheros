using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFicheros.Services;

namespace WebApiFicheros.Extensions
{
    public static class AplicationSeviceExtensions
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFicheroService,FicheroService>();
        }
    }
}