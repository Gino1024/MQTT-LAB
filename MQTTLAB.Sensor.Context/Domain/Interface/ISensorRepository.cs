namespace Sensor.Domain
{
  public interface ISensorRepository
  {
    public Task<SensorEntity> GetByID(Guid id);
    public Task<List<SensorEntity>> GetActiveSensors();
    public Task Save(SensorEntity instance);
    public void UpdateStatus(SensorEntity instance);
  }
}