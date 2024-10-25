using Microsoft.EntityFrameworkCore;

internal class CarListDbContext : DbContext
{
    public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
    {

    }

    public DbSet<Car> Cars { get; set; }
}