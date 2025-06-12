namespace Sensor.Domain
{

  public interface ISensorDataRepository
  {
    public Task Save(SensorDataEntity instance);
  }

}