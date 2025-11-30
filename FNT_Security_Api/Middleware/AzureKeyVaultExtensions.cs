namespace FNT_Security_Api.Middleware
{
    public static class AzureKeyVaultExtensions
    {
        public static WebApplicationBuilder AddServicesAzureKeyVault(this WebApplicationBuilder webApplicationBuilder)
        {
            var connectionAzureKeyVault = $"https://{webApplicationBuilder.Configuration["Vault"]}.vault.azure.net/";

            var credential = new ClientSecretCredential(
            tenantId: webApplicationBuilder.Configuration["AzureAd:TenantId"], // Azure AD Tenant ID
            clientId: webApplicationBuilder.Configuration["AzureAd:ClientId"],
            clientSecret: webApplicationBuilder.Configuration["AzureAd:ClientSecret"]);

            webApplicationBuilder.Configuration.AddAzureKeyVault(new Uri(connectionAzureKeyVault), credential);

            return webApplicationBuilder;
        }
    }
}
