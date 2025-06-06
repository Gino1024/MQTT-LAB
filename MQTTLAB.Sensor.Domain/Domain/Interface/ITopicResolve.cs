namespace Sensor.Domain
{
    public interface ITopicResolve
    {
        public string Resolve(string id, string type);
    }
}