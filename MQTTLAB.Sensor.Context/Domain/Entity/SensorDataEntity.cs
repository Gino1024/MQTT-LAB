using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sensor.Domain;

public class SensorDataEntity
{
  public static SensorDataEntity FromMqtt(string payload)
  {
    var data = JsonSerializer.Deserialize<SensorDataVO>(payload);
    SensorDataEntity entity = new SensorDataEntity();
    entity.SensorId = data.SensorID;
    entity.Timestamp = data.Timestamp;
    entity.Unit = data.Unit;
    entity.Value = data.Value;
    return entity;
  }
  [JsonPropertyName("id")]
  public Guid Id { get; set; }
  [JsonPropertyName("sensor_id")]
  public Guid SensorId { get; set; }
  [JsonPropertyName("timestamp")]
  public long Timestamp { get; set; }
  [JsonPropertyName("value")]
  public double Value { get; set; }
  [JsonPropertyName("unit")]
  public string Unit { get; set; }
}