using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoccoEmFrente.Kanban.Application.Repositories.Extensions
{
    public static class IServicesCollectionsExtensions
    {
        public static void AddAplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepositoy, ActivityRepositoy>();

        }

    }
}
