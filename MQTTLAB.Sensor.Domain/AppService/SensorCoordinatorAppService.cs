using Sensor.Domain;

namespace Sensor.AppService;

public class SensorCoordinatorAppService
{
  private ISensorFectory _sensorFectory;
  private SensorManager _sensorManager;
  public SensorCoordinatorAppService(ISensorFectory sensorFectory, SensorManager sensorManager)
  {
    _sensorFectory = sensorFectory;
    _sensorManager = sensorManager;
  }

  public void Simulation()
  {
    int count = 10;
    Parallel.ForEach(Enumerable.Range(0, count), (i) =>
    {
      var _ = Task.Run(async () =>
      {
        var sensor = _sensorFectory.CreateSimulateSensor();
        while (true)
        {
          await _sensorManager.SimulationAndPublish(sensor);
        }
      });
    });
  }
}


