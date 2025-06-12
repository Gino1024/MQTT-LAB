using System.Text.Json.Serialization;

namespace Sensor.Domain
{
    public class SensorData
    {
        [JsonPropertyName("sensor_id")]
        public Guid SensorID { get; set; }
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
        [JsonIgnore]
        public string Topic { get; set; }
    }
}