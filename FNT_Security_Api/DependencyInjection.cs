using FNT_Security_Api.Endpoints;

namespace FNT_Security_Api
{
    public static class DependencyInjection
    {
        public static IHostApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
        {
            builder.AddServicesAzureKeyVault();
            builder.AddConfigurationHttpJsonOptions();
            return builder;
        }

        private static WebApplicationBuilder AddConfigurationHttpJsonOptions(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureHttpJsonOptions(x =>
            {
                x.SerializerOptions.TypeInfoResolverChain.Insert(0, MenuSerializerContext.Default);
                
            });

            return builder;
        }
    }
}
