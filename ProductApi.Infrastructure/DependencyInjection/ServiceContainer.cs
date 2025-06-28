using ETickets.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Intefaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            SharedServiceContainer.AddSharedServices<MovieDbContext>(services, config, config["MySerilog:FileName"]!);
            services.AddScoped<IMovie, MovieRepository>();
            services.AddScoped<IActor, ActorRepository>();
            services.AddScoped<IProducer, ProducerRepository>();
            services.AddScoped<ICinema, CinemaRepository>();
            return services;
        }
        public static IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            MovieDbInitializer.Seed(app);
            return app;
        }
    }
}
