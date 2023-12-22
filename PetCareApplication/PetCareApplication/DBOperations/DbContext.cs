using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Models;

public class PetCareDbContext : DbContext
{
    public PetCareDbContext(DbContextOptions<PetCareDbContext> options) : base(options)
    {
    }
    public DbSet<Activity> Activity { get; set; }
    public DbSet<Food> Food { get; set; }
    public DbSet<HealthCondition> HealthCondition { get; set; }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<User> User { get; set; }  
    public DbSet<Training> Training { get; set; }

}

