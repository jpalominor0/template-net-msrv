
namespace FNT_Security_API.Middleware
{
    public class HandleHttpRequest : DelegatingHandler
    {
        public HandleHttpRequest() { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Constants.JWT != string.Empty)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWT);
            }

            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new SecurityException(await response.Content.ReadAsStringAsync());
                case System.Net.HttpStatusCode.ExpectationFailed:
                    throw new BussinessLogicException(await response.Content.ReadAsStringAsync());
                case System.Net.HttpStatusCode.InternalServerError:
                    throw new Exception(await response.Content.ReadAsStringAsync());
                default:
                    throw new Exception("Ocurrió un error inesperado, contactar con soporte técnico");
            }
        }
    }
}
