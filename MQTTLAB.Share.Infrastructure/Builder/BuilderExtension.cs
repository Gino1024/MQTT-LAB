using Infrastructrue.APINotifier;
using Infrastructrue.Database;
using Infrastructrue.Messaging.Mqtt;
using Infrastructrue.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTLAB.gRPC.Controller;
using Sensor.AppService;
using Sensor.Domain;
using Sensor.Infrastructrue;

namespace Infrastructure.Builder
{
  public static class BuilderExtesion
  {
    public static IServiceCollection AddDBService(this IServiceCollection services, IConfiguration configuration, ServiceLifetime lifetime)
    {
      // 注入 DbContext
      services.AddDbContext<MQTTLABDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), lifetime);
      if (lifetime == ServiceLifetime.Scoped)
      {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISensorRepository, SensorRepository>();
        services.AddScoped<ISensorDataRepository, SensorDataRepository>();
      }
      else
      {
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISensorRepository, SensorRepository>();
        services.AddSingleton<ISensorDataRepository, SensorDataRepository>();

      }


      return services;
    }

    public static IServiceCollection AddGrpcService(this IServiceCollection services, IConfiguration configuration)
    {
      // 注入 gRPC Client
      services.AddGrpcClient<SensorGrpc.SensorGrpcClient>(options =>
      {
        options.Address = new Uri(configuration["Grpc:SensorService"]);
      });

      return services;
    }
    public static IServiceCollection AddSensorContextService(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<IPublisher, MqttPublisher>();
      services.AddSingleton<ISensorFectory, SensorFectory>();
      services.AddSingleton<ITopicResolve, MqttTopicResolve>();
      services.AddSingleton<IAPINotifier, GrpcNotifier>();
      services.AddSingleton<ISubscribe, MqttSubscribe>();

      services.AddSingleton<MessageHandler>();
      services.AddSingleton<SensorManager>();
      services.AddSingleton<TemperatureSensorDataGenerator>();
      services.AddSingleton<WaterFlowSensorDataGenerator>();
      services.AddSingleton<PowerSensorDataGenerator>();
      services.AddSingleton<SensorCoordinatorAppService>();

      services.AddSingleton<IDictionary<SensorType, ISensorDataGenerator>>(sp =>
      {
        return new Dictionary<SensorType, ISensorDataGenerator>
          {
                            { SensorType.Temperature, sp.GetRequiredService<TemperatureSensorDataGenerator>() },
                            { SensorType.WaterFlow,   sp.GetRequiredService<WaterFlowSensorDataGenerator>() },
                            { SensorType.Power,       sp.GetRequiredService<PowerSensorDataGenerator>() }
          };
      });
      return services;
    }
  }
}