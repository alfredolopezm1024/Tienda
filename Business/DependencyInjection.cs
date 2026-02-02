using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ITiendaService, TiendaService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
