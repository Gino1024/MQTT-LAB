using Infrastructrue.Database;
using Microsoft.EntityFrameworkCore;
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

  public async Task Save(SensorEntity instance)
  {
    var sensor = new Infrastructrue.Database.Sensor();
    sensor.id = instance.Id;
    sensor.type = (int)instance.Type;
    sensor.status = (int)instance.Status;
    sensor.topic = instance.Topic;
    sensor.created_at = instance.createdAt;

    await _mqttlabDbContext.Sensors.AddAsync(sensor);
  }

  public async Task<List<SensorEntity>> GetActiveSensors()
  {
    var sensors = await _mqttlabDbContext.Sensors.Where(m => m.status == (int)SensorStatus.Running).ToListAsync();
    var sensorEntities = sensors.Select(m => new SensorEntity()
    {
      Id = m.id,
      Type = (SensorType)m.type,
      Status = (SensorStatus)m.status,
      Topic = m.topic,
      createdAt = m.created_at,
    }).ToList();

    return sensorEntities;
  }
}
