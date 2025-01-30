using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Persistence.Contexts;
using CM.Application.Interfaces;
using CM.Domain.Entities.PostEntities;
using CM.Infrastructure.Repositories;
using CM.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectrure.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddSingleton<DapperContext>();
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<ISelectedPostRepository, SelectedPostRepository>();
            services.AddDbContext<CMContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("CMContextConnection")));

            return services;
        }
    }
}
