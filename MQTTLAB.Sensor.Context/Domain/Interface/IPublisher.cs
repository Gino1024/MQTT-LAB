namespace Sensor.Domain
{
    public interface IPublisher
    {
        public Task Publish(string topic, string msg);
    }
}