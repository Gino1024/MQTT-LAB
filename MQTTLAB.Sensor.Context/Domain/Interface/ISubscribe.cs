namespace Sensor.Domain
{
    public interface ISubscribe
    {
        public Task Subscribe(string topic, string msg);
    }
}