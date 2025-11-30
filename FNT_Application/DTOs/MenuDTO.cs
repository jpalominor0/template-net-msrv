
namespace FNT_Application.DTOs
{
    public class MenuDTO
    {
        public string MenuId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Active { get; set; }

        public string CreatedUser { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string? ModifiedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? DescActive { get; set;} 
    }
}
