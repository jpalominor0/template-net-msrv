
namespace FNT_Persistence.Context;

public partial class SecurityContext : DbContext
{
    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options)
    {
    }   

    public virtual DbSet<Menu> Menus { get; set; }

    

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("MENU", "Security");

            entity.Property(e => e.MenuId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MENU_ID");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.CreatedUser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CREATED_USER");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIED_DATE");
            entity.Property(e => e.ModifiedUser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MODIFIED_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
