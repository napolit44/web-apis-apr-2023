using Microsoft.EntityFrameworkCore;

namespace HrApi.Domain;

public class HrDataContext : DbContext
{
    public HrDataContext(DbContextOptions<HrDataContext> options) : base(options)
    {

    }
    public DbSet<DepartmentEntity> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentEntity>().Property(p => p.Name)
            .HasMaxLength(20);
        modelBuilder.Entity<DepartmentEntity>()
            .HasIndex(p => p.Name).IsUnique();
    }
}
