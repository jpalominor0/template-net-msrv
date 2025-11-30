
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();
builder.AddServiceDefaults();

Secrets.Init(x =>
{
    x.EncryptionIV = builder.Configuration.GetValue<string>("Security:IV");
    x.EncryptionSalt = builder.Configuration.GetValue<string>("Security:Salt");
    x.EncryptionPassword = builder.Configuration.GetValue<string>("Security:Password");
    x.AzureAdTenantId = builder.Configuration.GetValue<string>("AzureAD:TenantID");
    x.AzureAdClientSecret = builder.Configuration.GetValue<string>("AzureAD:ClientSecret");
    x.AzureAdClientId = builder.Configuration.GetValue<string>("AzureAD:ClientId");
    x.AzureAdClientId = builder.Configuration.GetValue<string>("AzureAD:ClientId");
    //x.ApiScopes = configuration.GetValue<string>("DownstreamApi:Scopes");
    //x.ApiGraph = configuration.GetValue<string>("DownstreamApi:GraphApiUrl");    
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<HandleHttpRequest>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddInfraestructureService(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwagger();
builder.Services.AddCors(options =>
{
    var corsOriginsAllowed = builder.Configuration.GetSection("alloworigins").Get<List<string>>();
    var origins = corsOriginsAllowed != null ? corsOriginsAllowed.ToArray() : ["*"];
    options.AddPolicy("CorsPolicy",
        builder => builder
        .WithOrigins(origins)
        .AllowAnyMethod()
        .AllowAnyHeader());
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
Constants.IsInDevelopment = Convert.ToBoolean(builder.Configuration["IsInDevelopment"]);
app.UsePathBase(builder.Configuration["Information:PathBase"] ?? "/");

app.UseSwaggerDocumentation();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();


app.MapMenu();
app.UseCors("CorsPolicy");

try
{
    Log.Logger = LogginConfiguration.AddLoggerConfiguration(app);
    Serilog.Debugging.SelfLog.Enable(msg =>
    {
        Debug.Print(msg);
    });
    if (Constants.IsInDevelopment)
    {
        Serilog.Debugging.SelfLog.Enable(msg =>
        {
            Debug.Print(msg);
        });
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}


