using WebApplication1.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Quadra> Quadras => Set<Quadra>();
    public DbSet<Reserva> Reserva => Set<Reserva>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=beachtennis.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Quadra)
            .WithMany(q => q.Reservas)
            .HasForeignKey(r => r.QuadraID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}