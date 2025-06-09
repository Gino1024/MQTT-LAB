namespace Sensor.Domain;

public interface IAPINotifier
{
  public Task NotifySensorRegisterAsync(SensorEntity sensor);
}
