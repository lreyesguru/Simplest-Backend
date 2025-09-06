using Microsoft.Extensions.DependencyInjection;

namespace Invoice.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IManageInvoicesUseCase<string>, ManageInvoicesUseCase>();
            return services;
        }
    }
}
