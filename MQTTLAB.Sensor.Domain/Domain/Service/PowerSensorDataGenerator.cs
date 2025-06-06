namespace Sensor.Domain
{
    public class PowerSensorDataGenerator : ISensorDataGenerator
    {
        private readonly Random _random = new Random();
        public double GeneratorValue()
        {
            double hours = DateTime.UtcNow.Hour + DateTime.UtcNow.Minute / 60.0;
            double sineComponent = Math.Sin(hours / 24.0 * 2 * Math.PI);

            double basePower = 5.0 + 3.0 * sineComponent;

            double noise = (_random.NextDouble() - 0.5) * 1.0;

            double result = basePower + noise;

            if (result < 0.0) result = 0.0;
            if (result > 10.0) result = 10.0;

            return result;
        }
    }
}