using DriveAnheu.Infrastructure.UnitOfWork.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Infrastructure.UnitOfWork
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnityOfWorkService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}