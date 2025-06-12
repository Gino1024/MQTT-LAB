namespace Sensor.Domain
{
    public class WaterFlowSensorDataGenerator : ISensorDataGenerator
    {
        private readonly Random _random = new Random();
        public double GeneratorValue()
        {
            // 假设低峰在 0~20 L/min，高峰在 60~100 L/min
            // 先用一个随机概率决定是否进入高峰
            double peakChance = _random.NextDouble();
            if (peakChance < 0.2)
            {
                // 20% 概率走低峰：0~20
                return _random.NextDouble() * 20.0;
            }
            else if (peakChance < 0.8)
            {
                // 60% 概率走中间：20~60
                return 20.0 + _random.NextDouble() * 40.0;
            }
            else
            {
                // 20% 概率走高峰：60~100
                return 60.0 + _random.NextDouble() * 40.0;
            }
        }
    }
}