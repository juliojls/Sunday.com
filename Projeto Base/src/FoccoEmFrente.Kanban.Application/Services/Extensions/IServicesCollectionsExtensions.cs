using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoccoEmFrente.Kanban.Application.Services.Extensions
{
    public static class IServicesCollectionsExtensions 
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IActivityService, ActivityService>();
        }
    }
}
