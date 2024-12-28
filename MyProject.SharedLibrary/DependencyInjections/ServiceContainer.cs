using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Data;
using MyProject.Repositories.Departments;
using MyProject.Repositories.Employees;
using MyProject.Repositories.PerformanceReviews;
using MyProject.Repository.Abstructions.Departments;
using MyProject.Repository.Abstructions.Employees;
using MyProject.Repository.Abstructions.PerformanceReviews;
using MyProject.Services.Absructions.Departments;
using MyProject.Services.Absructions.Employees;
using MyProject.Services.Absructions.PerformanceReviews;
using MyProject.Services.Departments;
using MyProject.Services.Employees;
using MyProject.Services.PerformanceReviews;

namespace MyProject.SharedLibrary.DependencyInjections
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            //Add database dependency
            #region Database
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(config.GetConnectionString("AppConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            #endregion

            //Add dependency injection (DI)
            #region Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPerformanceService, PerformanceService>();
            #endregion

            #region Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            #endregion

            return services;
        }

    }
}
