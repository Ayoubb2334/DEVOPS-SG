using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Smartphone> Smartphones => Set<Smartphone>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Smartphone>(entity =>
        {
            entity.ToTable("smartphones");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Marque).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Modele).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Prix).HasColumnType("decimal(10,2)");
            entity.Property(s => s.Description).HasMaxLength(1000);
        });

        base.OnModelCreating(modelBuilder);
    }
}