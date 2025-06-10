using Infrastructrue.Database;
using Infrastructrue.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTLAB.gRPC.Controller;
using Sensor.Domain;

namespace Infrastructure.Builder
{
  public static class BuilderExtesion
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      // 注入 DbContext
      services.AddDbContext<MQTTLABDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<ISensorRepository, SensorRepository>();

      // 注入 gRPC Client
      services.AddGrpcClient<SensorGrpc.SensorGrpcClient>(options =>
      {
        options.Address = new Uri(configuration["Grpc:SensorService"]);
      });

      return services;
    }
  }
}