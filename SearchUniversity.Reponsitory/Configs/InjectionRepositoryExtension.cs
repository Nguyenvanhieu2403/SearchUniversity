using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchUniversity.Reponsitory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Configs
{
    public static class InjectionRepositoryExtension
    {
        public static void DependencyInjectionRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUser, UserRepons>();
            services.AddScoped<IUniversityRepons, UniversityRepons>();
            services.AddScoped<IArea, AreaRepons>();
            services.AddScoped<IAdmissionsMethod, AdmissionxsMethodRepons>();
            services.AddScoped<IDepartments, DepartmentsRepons>();
            services.AddScoped<IBenchmark, BenchmarkRepons>();
        }
    }
}
