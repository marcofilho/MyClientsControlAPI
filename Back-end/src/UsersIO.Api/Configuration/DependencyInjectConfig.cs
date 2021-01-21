using Microsoft.Extensions.DependencyInjection;
using UsersIO.Business.Interfaces;
using UsersIO.Business.Notifications;
using UsersIO.Business.Services;
using UsersIO.Data.Context;
using UsersIO.Data.Repositories;

namespace UsersIO.Api.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<ProjectDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<ISocialMidiaRepository, SocialMidiaRepository>();

            services.AddScoped<INotificator, Notificator>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
