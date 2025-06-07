using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Sensor.Domain
{
    public class SensorManager
    {
        private readonly ILogger<SensorManager> _logger;
        private IDictionary<SensorType, ISensorDataGenerator> _sensorDataGenerator;
        private IPublisher _publisher;
        private ITopicResolve _topicResolver;
        public SensorManager(IDictionary<SensorType, ISensorDataGenerator> sensorDataGenerator, IPublisher publisher, ITopicResolve topicResolver, ILogger<SensorManager> logger)
        {
            _logger = logger;
            _sensorDataGenerator = sensorDataGenerator;
            _publisher = publisher;
            _topicResolver = topicResolver;
        }
        public async Task SimulationAndPublish(SensorEntity sensor)
        {
            //建立模擬資料
            var data = new SensorData()
            {
                Id = sensor.Id,
                Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),
                Unit = sensor.Type switch
                {
                    SensorType.Temperature => "C",
                    SensorType.WaterFlow => "L/min",
                    SensorType.Power => "kW",
                    _ => ""
                },
                Value = _sensorDataGenerator[sensor.Type.Value].GeneratorValue(),
                Topic = _topicResolver.Resolve(sensor.Id.ToString(), sensor.Type.ToString())
            };

            var payload = JsonSerializer.Serialize(data);

            //發佈
            await _publisher.Publish(payload, data.Topic);
            await Task.Delay(5000);
        }
    }
}