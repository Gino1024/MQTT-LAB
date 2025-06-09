using Sensor.Domain;

namespace Infrastructrue.Messaging.Mqtt;

public class MqttTopicResolve : ITopicResolve
{
    public string Resolve(string id, string type)
    {
        return $"device/{type}/{id}";
    }
}