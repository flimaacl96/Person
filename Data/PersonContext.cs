using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options)  {}
    public DbSet<PersonModel> People { get; set; }

    public DbSet<PositionModel> Position { get; set; }
        
    public DbSet<EmployeeModel> Employee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<EmployeeModel>(entity =>
    {
        entity.HasKey(e => new { e.PersonId, e.PositionId });
        entity.ToTable("Employee");

        entity.HasOne(e => e.Person)
              .WithMany(p => p.Employees)
              .HasForeignKey(e => e.PersonId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Position)
              .WithMany(p => p.Employees)
              .HasForeignKey(e => e.PositionId)
              .OnDelete(DeleteBehavior.Restrict);
    });
}

}