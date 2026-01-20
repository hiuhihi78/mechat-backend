using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Infrastructure.Persistence.Interceptors;
using MeChat.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.Persistence.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        // HttpContext
        services.AddHttpContextAccessor();

        // Interceptor
        services.AddSingleton<AuditTableEntitiesInterceptor>();

        // DbContext
        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            options
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(
                    configuration.GetConnectionString("SqlServer"),
                    sql =>
                        sql.MigrationsAssembly(
                            typeof(ApplicationDbContext).Assembly.GetName().Name))
                .AddInterceptors(sp.GetRequiredService<AuditTableEntitiesInterceptor>());
        });

        // Repository & UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
