using Infrastructrue.Database;
using Sensor.Domain;
public class SensorRepository : ISensorRepository
{
  public readonly MQTTLABDbContext _mqttlabDbContext;

  public SensorRepository(MQTTLABDbContext mqttlabDbContext)
  {
    _mqttlabDbContext = mqttlabDbContext;
  }

  public void UpdateStatus(SensorEntity instance)
  {
    var sensor = new Infrastructrue.Database.Sensor();
    sensor.id = instance.Id;
    sensor.status = (int)instance.Status;
    _mqttlabDbContext.Attach(sensor);
    _mqttlabDbContext.Entry(sensor).Property(x => x.status).IsModified = true;
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
    sensor.created_at = instance.createdAt;

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
