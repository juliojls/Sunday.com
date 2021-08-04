using FoccoEmFrente.Kanban.Application.Repositories.Extensions;
using FoccoEmFrente.Kanban.Application.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FoccoEmFrente.Kanban.Web
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAplicationRepositories();
            serviceCollection.AddAplicationServices();
        }
    }
}
