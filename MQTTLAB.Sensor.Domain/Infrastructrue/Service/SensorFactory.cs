using Sensor.Domain;

namespace Sensor.Infrastructrue
{
    public class SensorFectory : ISensorFectory
    {
        /// <summary>
        /// 取得模擬Sensor
        /// </summary>
        /// <param name="count">數量</param>
        /// <returns></returns>
        public SensorEntity CreateSimulateSensor()
        {
            Guid guid = Guid.NewGuid();
            SensorType type = this.RadomSensorType();

            SensorEntity sensor = new SensorEntity(guid, type, SensorStatus.Stopped);

            return sensor;
        }

        private SensorType RadomSensorType()
        {
            Random r = new Random();
            // 取得所有列舉值
            Array values = Enum.GetValues(typeof(SensorType));

            // 隨機索引
            int index = r.Next(values.Length);

            // 轉回 SensorType
            SensorType randomType = (SensorType)values.GetValue(index);

            return randomType;
        }
    }
}