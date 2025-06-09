using Microsoft.Extensions.Logging;
using Sensor.Domain;

namespace Sensor.AppService;

public class SensorCoordinatorAppService
{

  private readonly ILogger<SensorCoordinatorAppService> _logger;
  private ISensorFectory _sensorFectory;
  private IAPINotifier _apiNotifier;
  private SensorManager _sensorManager;
  public SensorCoordinatorAppService(ISensorFectory sensorFectory, IAPINotifier apiNotifier, SensorManager sensorManager, ILogger<SensorCoordinatorAppService> logger)
  {
    _sensorFectory = sensorFectory;
    _apiNotifier = apiNotifier;
    _sensorManager = sensorManager;
    _logger = logger;
  }

  public void Simulation()
  {
    int count = 10;
    Parallel.ForEach(Enumerable.Range(0, count), (i) =>
    {
      var _ = Task.Run(async () =>
      {
        var sensor = _sensorFectory.CreateSimulateSensor();
        await _apiNotifier.NotifySensorRegisterAsync(sensor);

        sensor.Active();
        //更新Server

        //透過Manger執行傳送模擬資料
        while (true && sensor.Status == SensorStatus.Running)
        {
          await _sensorManager.SimulationAndPublish(sensor);
        }
      });
    });
  }
}


