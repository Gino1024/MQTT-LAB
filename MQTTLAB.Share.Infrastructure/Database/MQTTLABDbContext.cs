using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Database;

public class MQTTLABDbContext : DbContext
{
  public DbSet<Sensor> Products { get; set; }

  public MQTTLABDbContext(DbContextOptions<MQTTLABDbContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // 🔹 User 設定
    modelBuilder.Entity<Sensor>(entity =>
    {
      entity.ToTable("t_sensor");

      entity.HasKey(e => e.id);
      entity.Property(e => e.id).ValueGeneratedOnAdd();

      entity.Property(e => e.key)
            .IsRequired()
            .HasMaxLength(32);

      entity.Property(e => e.type)
            .IsRequired()
            .HasMaxLength(1);

      entity.Property(e => e.status)
          .IsRequired()
          .HasMaxLength(1);

      entity.HasIndex(i => i.key).IsUnique();
    });
  }

}
