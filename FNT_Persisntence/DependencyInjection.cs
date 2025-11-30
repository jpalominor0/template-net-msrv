namespace FNT_Persistence
{
    public static class DependencyInjection
    {
        private static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        public static IServiceCollection AddInfraestructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceDependency(configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            return services;
        }

        private static IServiceCollection AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SecurityContext>(x =>
            {
                x.UseLoggerFactory(loggerFactory);
                x.EnableSensitiveDataLogging();
                x.EnableDetailedErrors().LogTo(Console.WriteLine, LogLevel.Debug);

                x.UseSqlServer(GetConnectionString(configuration));
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("configuration", "database connection DefaultConnection doesn't exists in AppSettings");
        }
    }
}
