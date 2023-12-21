using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;

public class PetCareDbContext : DbContext
{
    public PetCareDbContext(DbContextOptions<PetCareDbContext> options) : base(options)
    {
    }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Food> Food { get; set; }
    public DbSet<HealthCondition> HealthCondition { get; set; }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<User> User { get; set; }    

}

