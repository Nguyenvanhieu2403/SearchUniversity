using SearchUniversity.Reponsitory.Configs;

namespace SearchUniversity.Controller.StartUp
{
    public static class ConfigServices
    {
        public static void AddCustomService(this IServiceCollection services, IConfiguration configuration)
        {
            services.DependencyInjectionRepository(configuration);
        }
    }
}
