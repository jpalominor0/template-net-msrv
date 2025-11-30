
namespace FNT_CrossCutting
{
    public static class Secrets
    {
        public static SecretsEntity secretos = null;

        public static void Init(Action<SecretsEntity>? Configuration)
        {
            secretos = new SecretsEntity();
            Configuration?.Invoke(secretos);
        }
    }

    public class SecretsEntity
    {
        public string? ApiScopes { get; set; }
        public string? ApiGraph { get; set; }
        public string? DatabaseConnection { get; set; }
        public string? ApinConecctionString { get; set; }
        public string? BlobServiceConnectionString { get; set; }
        public string? StorageAccountKey { get; set; }
        public string? StorageAccountName { get; set; }
        public decimal? AttachmentMaxSize { get; set; }
        public string? AttachmentPath { get; set; }
        public string? AzureAdClientId { get; set; }
        public string? AzureAdClientSecret { get; set; }
        public string? AzureAdDomain { get; set; }
        public string? AzureAdTenantId { get; set; }
        public string? TokenSecurity { get; set; }
        public string? EncryptionPassword { get; set; }
        public string? EncryptionSalt { get; set; }
        public string? EncryptionIV { get; set; }
        public string? AppName { get; set; }
    }
}
