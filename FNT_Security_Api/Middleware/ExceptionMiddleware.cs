
using Microsoft.AspNetCore.Authorization;

namespace FNT_Security_API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private static ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ExceptionMiddleware> logger, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var traceidentifier = Guid.NewGuid().ToString();
            Constants.TraceIdentifier = traceidentifier;

            try
            {
                var endpoint = httpContext.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                {
                    await _next(httpContext);
                    return;
                }

                httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationfromheader);

                var token = authorizationfromheader.Count > 0 ? authorizationfromheader[0] : string.Empty;

                if (!string.IsNullOrEmpty(token))
                {
                    token = token?.Split(" ").Last();
                    Constants.Username = "";//JwtHelper.ValidateToken(token, _configuration["JwtConfig:Secret"] ?? throw new ArgumentNullException("Not Passing JWT secret")); // aqui deberia leer el endpoint para JWT
                    Constants.JWT = token;
                    
                    await EnrichLogWithHttpContext(httpContext);
                    await _next.Invoke(httpContext);
                }
                else
                {
                    httpContext.Response.Clear();
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    await httpContext.Response.WriteAsync("Unauthorized for this request.");
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                await HandleTokenExpiredException(httpContext, traceidentifier, ex);
            }
            catch (BussinessLogicException ex)
            {
                await HandleBusinessLogicException(httpContext, ex, traceidentifier);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }

        }

        private static async Task EnrichLogWithHttpContext(HttpContext httpContext) 
        {
            LogContext.PushProperty("AppName", Secrets.secretos.AppName);
            LogContext.PushProperty("Traceidentifier", Constants.TraceIdentifier);
            LogContext.PushProperty("Username", Constants.Username);

            if (httpContext.Request.QueryString.HasValue)
            {
                Constants.QueryString = httpContext.Request.QueryString.Value;
            }

            if (httpContext.Request.RouteValues.Any())
            {
                var routeParameters = new List<string>();
                foreach (var routeParam in httpContext.Request.RouteValues)
                {
                    routeParameters.Add($"{routeParam.Key}:{ routeParam.Value?.ToString()}");
                }

                Constants.RouteParams = routeParameters.Serialize();
            }

            if (httpContext.Request.Method == HttpMethods.Post || httpContext.Request.Method == HttpMethods.Put)
            {
                httpContext.Request.EnableBuffering();

                var requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

                httpContext.Request.Body.Position = 0;

                if (!string.IsNullOrEmpty(requestBody))
                {
                    Constants.RequestBody = requestBody;
                }
            }
        }

        private static async Task HandleBusinessLogicException(HttpContext context, BussinessLogicException ex, string traceindetifier)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;

            await context.Response.WriteAsync(ex.Message);
        }

        private static async Task HandleTokenExpiredException(HttpContext context, string traceidentifier, SecurityTokenExpiredException ex)
        {
            
            context.Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;

            _logger.LogCritical(ex, "({@AppName} -> [{@Username}] {@TraceIdentifier} - Token is expired for {@Username}",
                Secrets.secretos.AppName,
                Constants.Username,
                traceidentifier,
                Constants.Username);

            await context.Response.WriteAsync("El token expiró");
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogCritical(ex, "({@AppName} -> [{@Username}] {@TraceIdentifier} - {@Message} | {@QueryString} | {@RequestBody} | {@RouteParams}",
                Secrets.secretos.AppName,
                Constants.Username,
                Constants.TraceIdentifier,
                ex.Message,
                Constants.QueryString,
                Constants.RequestBody,
                Constants.RouteParams);

            var message = Constants.IsInDevelopment ? ex.Message : string.Format(Message.UnexpectedErrorMessage, Constants.TraceIdentifier);

            await context.Response.WriteAsync(message);
        }
    }

    public class AllowAnonymousAttribute : Attribute, IAllowAnonymous { }
}
