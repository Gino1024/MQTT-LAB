using Sensor.Domain;
using Sensor.Infrastructrue;
using Infrastructrue.Messaging.Mqtt;
using Sensor.AppService;
using Serilog;
using Serilog.Formatting.Json;
using Infrastructure.Builder;
using Infrastructrue.APINotifier;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sensor.GateworkWorker
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
                      .WriteTo.Console()
                      .WriteTo.RabbitMQ((clientConfig, sinkConfig) =>
                      {
                        // RabbitMQ 連線設定
                        clientConfig.Username = "guest";                  // 或你的帳號
                        clientConfig.Password = "guest";                  // 或你的密碼
                        clientConfig.VHost = "/";                   // 預設 vhost
                        clientConfig.Hostnames = new[] { "localhost" };   // RabbitMQ 主機位址
                        clientConfig.Port = 5672;                         // AMQP port
                        clientConfig.Exchange = "";                       // 空字串 = 預設交換器
                        clientConfig.RoutingKey = "MQTT-Lab-Queue";         // 路由鍵設為已存在的佇列名稱
                        clientConfig.DeliveryMode = Serilog.Sinks.RabbitMQ.RabbitMQDeliveryMode.Durable;

                        // 日誌格式（可自訂為 PlainTextFormatter、JsonFormatter 等）
                        sinkConfig.TextFormatter = new JsonFormatter();
                      })
                      .CreateLogger();

      // 建立一個 Generic Host，並且註冊我們剛剛建立的 Worker
      var host = Host.CreateDefaultBuilder(args)
          .UseSerilog()
          // 如果你想要把它註冊成 Windows Service 或 Linux systemd：
          // .UseWindowsService()   // 在 Windows 環境下改成 Windows Service
          // .UseSystemd()         // 在 Linux 環境下改成 systemd 服務
          .ConfigureServices((hostContext, services) =>
          {
            // 註冊 BackgroundService
            services.AddHostedService<GatewayWorker>();
            services.AddDBService(hostContext.Configuration, ServiceLifetime.Singleton);
            services.AddGrpcService(hostContext.Configuration);
            services.AddSensorContextService(hostContext.Configuration);
          })
          .ConfigureLogging(logging =>
          {
            logging.ClearProviders();
            logging.AddConsole();
            // 你可以依照需求再加上 Debug、EventLog 等 provider
          })
          .Build();


      await host.RunAsync();
    }
  }
}
