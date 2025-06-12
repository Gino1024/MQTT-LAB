namespace Sensor.Domain;

public interface IAPINotifier
{
  public Task<string> NotifySensorRegisterAsync(SensorEntity sensor);
  public Task<string> UpdateStatusAsync(SensorEntity sensor);


}
