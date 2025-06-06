using Sensor.Domain;

namespace Infrastructrue;

public class MqttTopicResolve : ITopicResolve
{
    public string Resolve(string id, string type)
    {
        return $"device/{type}/{id}";
    }
}