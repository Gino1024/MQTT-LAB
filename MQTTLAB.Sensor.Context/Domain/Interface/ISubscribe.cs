namespace Sensor.Domain
{
    public interface ISubscribe
    {
        public Task ConnectAsync();
        public Task SubscribeAsync(string topic, Func<string, Task> handler);
    }
}