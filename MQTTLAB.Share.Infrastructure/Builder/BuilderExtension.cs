using Infrastructrue.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTLAB.gRPC.Controller;

namespace Infrastructure.Builder
{
  public static class BuilderExtesion
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      // 注入 DbContext
      services.AddDbContext<MQTTLABDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      // 注入 gRPC Client
      services.AddGrpcClient<Greeter.GreeterClient>(options =>
      {
        options.Address = new Uri(configuration["Grpc:SensorService"]);
      });

      return services;
    }
  }
}