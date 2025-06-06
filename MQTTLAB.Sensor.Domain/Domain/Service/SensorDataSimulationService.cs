using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Sensor.Domain
{
    public class SensorDataSimulationService
    {
        private ISensorFectory _sensorRepository;
        private IDictionary<SensorType, ISensorDataGenerator> _sensorDataGenerator;
        private IPublisher _publisher;
        private ITopicResolve _topicResolver;
        public SensorDataSimulationService(ISensorFectory sensorRepository, IDictionary<SensorType, ISensorDataGenerator> sensorDataGenerator, IPublisher publisher, ITopicResolve topicResolver)
        {
            _sensorRepository = sensorRepository;
            _sensorDataGenerator = sensorDataGenerator;
            _publisher = publisher;
            _topicResolver = topicResolver;
        }
        public async Task SimulationAndPublish()
        {
            //取得Entity
            var entity = _sensorRepository.CreateSimulateSensor();

            if (!_sensorDataGenerator.ContainsKey(entity.Type.Value))
                throw new Exception($"Sensor 建立失敗: SensorType {entity.Type} 不存在");


            //建立模擬資料
            var data = new SensorData();
            data.Id = entity.Id;
            data.Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            data.Unit = entity.Type switch
            {
                SensorType.Temperature => "C",
                SensorType.WaterFlow => "L/min",
                SensorType.Power => "kW",
                _ => ""
            };

            data.Value = _sensorDataGenerator[entity.Type.Value].GeneratorValue();
            data.Topic = _topicResolver.Resolve(entity.Id.ToString(), entity.Type.ToString());
            var payload = JsonSerializer.Serialize(data);

            //發佈
            await _publisher.Publish(payload, data.Topic);
            Console.WriteLine($"topic: {data.Topic}, payload: {payload}");
            await Task.Delay(1000);
        }

        /// <summary>
        /// 解析單位
        /// </summary>
        /// <returns></returns>
        private string ResolveUnit(SensorType? type)
        {
            string result = string.Empty;

            return result;
        }
    }
}