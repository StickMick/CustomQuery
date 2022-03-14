using CustomQuery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CustomQuery.Data;

public class Context: DbContext
{
    public Context(
        DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBst>()
            .HasKey(x => new { x.BstId, x.UserId });
    }
    
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<UserBst> UserBsts { get; set; }
}