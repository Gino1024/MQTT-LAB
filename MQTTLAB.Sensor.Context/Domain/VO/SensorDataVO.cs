using System.Text.Json.Serialization;

namespace Sensor.Domain
{
    public class SensorDataVO
    {
        [JsonPropertyName("sensor_id")]
        public Guid SensorID { get; set; }
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}