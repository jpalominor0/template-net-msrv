using Microsoft.OpenApi.Models;

namespace FNT_Security_API.Middleware
{
    public static class SwaggerMiddleware
    {
        public static void AddSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "Security Endpoints",
                    Description = "Page for security endpoints",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Template Continental"
                    }
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Description = "JWT Authotization header using the bearer scheme. Example: \"Authorization Bearer {TokenValue}\"",
                    Name = "Authorization",
                    Scheme = "bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()

                     }
                });
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Constants.SwaggerJsonPath, "Security API");
            });
        }
    }
}
