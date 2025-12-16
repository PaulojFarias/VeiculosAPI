using Microsoft.EntityFrameworkCore;
using VeiculosAPI.Domain.Entities;

namespace VeiculosAPI.Infra.Context;

public class VeiculosDbContext : DbContext
{
    public VeiculosDbContext(DbContextOptions<VeiculosDbContext> options) : base(options) { }

    public DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Veiculo>()
            .HasKey(v => v.Id);

        modelBuilder.Entity<Veiculo>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
    }
}