namespace FNT_Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMapster();
            MapperConfiguration.Configure();
            services.AddScoped<MenuHandler>();
            return services;
        }


    }
}
