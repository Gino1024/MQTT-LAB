namespace Sensor.Domain
{
  public interface ISensorRepository
  {
    public Task<SensorEntity> GetByID(Guid id);
    public Task Insert(SensorEntity instance);
    public void Update(SensorEntity instance);
  }
}