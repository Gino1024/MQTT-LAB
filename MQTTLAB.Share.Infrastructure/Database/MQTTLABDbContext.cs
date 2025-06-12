using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Database;

public class MQTTLABDbContext : DbContext
{
  public DbSet<Sensor> Sensors { get; set; }

  public MQTTLABDbContext(DbContextOptions<MQTTLABDbContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Sensor>(entity =>
    {
      entity.ToTable("t_sensor");

      entity.HasKey(e => e.id);

      entity.Property(e => e.type)
            .IsRequired()
            .HasMaxLength(1);

      entity.Property(e => e.status)
          .IsRequired()
          .HasMaxLength(1);

      entity.HasMany(e => e.SensorDatas)
            .WithOne(e => e.sensor)
            .HasForeignKey(e => e.sensor_id)
            .HasPrincipalKey(e => e.id);
    });

    modelBuilder.Entity<SensorData>(entity =>
    {
      entity.ToTable("t_sensor_data");

      entity.HasKey(e => e.id);
      entity.Property(e => e.sensor_id)
            .IsRequired();
    });
  }

}
