using ChatApp.DataAccess.Repositories.Implementations;
using ChatApp.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.DataAccess.Extensions;

public static class ApplicationDependenciesConfiguration
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<ChatContext>(options);
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IContactRepository, ContactRepostiory>()
            .AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }
}
