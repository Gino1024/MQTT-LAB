using System.Text.Json.Serialization;
namespace Sensor.Domain;

public class SensorDataEntity
{
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
  [JsonIgnore]
  public string Topic { get; set; }
}