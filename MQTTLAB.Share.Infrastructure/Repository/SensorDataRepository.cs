using Infrastructrue.Database;
using Sensor.Domain;
public class SensorDataRepository : ISensorDataRepository
{
  public readonly MQTTLABDbContext _mqttlabDbContext;

  public SensorDataRepository(MQTTLABDbContext mqttlabDbContext)
  {
    _mqttlabDbContext = mqttlabDbContext;
  }

  public async Task Save(SensorDataEntity instance)
  {
    SensorData entity = new SensorData()
    {
      sensor_id = instance.SensorId,
      value = instance.Value,
      unit = instance.Unit,
      created_at = instance.Timestamp,
    };
    await _mqttlabDbContext.SensorDatas.AddAsync(entity);

  }
}
