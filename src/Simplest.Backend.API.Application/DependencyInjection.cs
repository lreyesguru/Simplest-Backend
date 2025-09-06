using Microsoft.Extensions.DependencyInjection;

namespace Simplest.Backend.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IManageInvoicesUseCase, ManageInvoicesUseCase>();
            return services;
        }
    }
}
