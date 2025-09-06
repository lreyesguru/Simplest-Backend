using Invoice.API.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(conn => new SqlConnection(connectionString));
            services.AddScoped<IInvoiceRepository<string>, InvoiceRepository>();
            return services;
        }
    }
}
