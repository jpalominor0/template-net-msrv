
namespace FNT_Application.DTOs
{
    public  class CredentialsDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
