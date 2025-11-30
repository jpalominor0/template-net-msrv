namespace FNT_Domain.MenuAggregates;

public partial class Menu
{
    public string MenuId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Active { get; set; }

    public string CreatedUser { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    
}
