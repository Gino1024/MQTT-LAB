using Microsoft.Extensions.Logging;
using Sensor.Domain;

namespace Sensor.AppService;

public class SensorCoordinatorAppService
{

  private readonly ILogger<SensorCoordinatorAppService> _logger;
  private ISensorFectory _sensorFectory;
  private SensorManager _sensorManager;
  public SensorCoordinatorAppService(ISensorFectory sensorFectory, SensorManager sensorManager, ILogger<SensorCoordinatorAppService> logger)
  {
    _sensorFectory = sensorFectory;
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
        _logger.LogInformation($"created {sensor.Type}, {sensor.Id}");
        while (true)
        {
          await _sensorManager.SimulationAndPublish(sensor);
        }
      });
    });
  }
}


