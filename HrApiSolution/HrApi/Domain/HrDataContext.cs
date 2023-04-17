using Microsoft.EntityFrameworkCore;

namespace HrApi.Domain;

public class HrDataContext : DbContext
{
    public HrDataContext(DbContextOptions<HrDataContext> options) : base(options)
    {

    }
    public DbSet<DepartmentEntity> Departments { get; set; }
}
