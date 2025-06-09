using Infrastructrue.Database;
using Sensor.Domain;
public class SensorRepository : ISensorRepository
{
  public readonly MQTTLABDbContext _mqttlabDbContext;

  public SensorRepository(MQTTLABDbContext mqttlabDbContext)
  {
    _mqttlabDbContext = mqttlabDbContext;
  }
  public async Task<SensorEntity> GetByID(Guid id)
  {
    var data = await _mqttlabDbContext.Sensors.FindAsync(id);
    SensorEntity sensor = new SensorEntity(data.id, (SensorType)data.type, (SensorStatus)data.status);

    return sensor;
  }

  public async Task Insert(SensorEntity instance)
  {
    var sensor = new Infrastructrue.Database.Sensor();
    sensor.id = instance.Id;
    sensor.type = (int)instance.Type;
    sensor.status = (int)instance.Status;

    await _mqttlabDbContext.Sensors.AddAsync(sensor);
  }
  public void Update(SensorEntity instance)
  {
    var sensor = new Infrastructrue.Database.Sensor();
    sensor.id = instance.Id;
    sensor.type = (int)instance.Type;
    sensor.status = (int)instance.Status;

    _mqttlabDbContext.Sensors.Update(sensor);
  }
}
