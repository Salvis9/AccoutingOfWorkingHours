using DAL.Interceptors;
using DAL.Repositories;
using Domain.Entity;
using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresSQL");

            services.AddSingleton<DateInterceptors>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.InitRepositories();
        }
        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
            services.AddScoped<IBaseRepository<TaskEntity>, BaseRepository<TaskEntity>>();
            services.AddScoped<IBaseRepository<TimeSheet>, BaseRepository<TimeSheet>>();
        }
    }
}
