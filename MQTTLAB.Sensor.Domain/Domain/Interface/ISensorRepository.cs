namespace Sensor.Domain
{
  public interface ISensorRepository
  {
    public bool Save(SensorEntity sensor);
    public SensorEntity GetByID(string id);
  }
}