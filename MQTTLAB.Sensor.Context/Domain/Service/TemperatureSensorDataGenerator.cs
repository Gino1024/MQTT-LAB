namespace Sensor.Domain
{
    public class TemperatureSensorDataGenerator : ISensorDataGenerator
    {
        private readonly Random _random = new Random();
        public double GeneratorValue()
        {
            double baseTemp = 20 + 10 * Math.Sin(DateTime.UtcNow.Minute / 60.0 * 2 * Math.PI);
            double noise = _random.NextDouble() * 2 - 1; // -1~1
            return baseTemp + noise;
        }
    }
}