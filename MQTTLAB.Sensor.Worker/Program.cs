using Sensor.Domain;
using Sensor.Infrastructrue;
using Infrastructrue;
using Sensor.AppService;
using Serilog;

namespace Sensor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
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
                    services.AddHostedService<Worker>();

                    // 如果原本有其他依賴的 service，這裡也可以一併註冊
                    services.AddSingleton<IPublisher, MqttPublisher>();
                    services.AddSingleton<ISensorFectory, SensorFectory>();
                    services.AddSingleton<ITopicResolve, MqttTopicResolve>();

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
