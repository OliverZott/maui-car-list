using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

internal class CarListDbContext : IdentityDbContext
{
    public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
    {

    }

    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Make = "BMW",
                Model = "5",
                Vin = "bmwvin1"
            },
            new Car
            {
                Id = 2,
                Make = "Subaru",
                Model = "Impreza",
                Vin = "subaruvin1"
            },
            new Car
            {
                Id = 3,
                Make = "Honda",
                Model = "Civic",
                Vin = "hondavin1"
            }
        );
    }

}