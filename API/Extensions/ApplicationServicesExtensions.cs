using Application.Core;
using Application.Users.Commands;
using MediatR;
using Persistence;
using Persistence.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>();
            services.AddCors(opt =>
            {
                opt.AddPolicy(Constants.CORS_POLICY, policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(Constants.APP_BASE_URI);
                });
            });

            services.AddScoped<IDataContext, DataContext>(
                provider => provider.GetService<DataContext>());
            services.AddMediatR(typeof(Edit).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}
