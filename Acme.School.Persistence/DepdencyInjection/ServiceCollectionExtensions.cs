using Microsoft.Extensions.DependencyInjection;

namespace Acme.School.Persistence.DepdencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            //services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
