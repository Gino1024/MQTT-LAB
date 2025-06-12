namespace Sensor.Domain
{
    public interface ISensorFectory
    {
        /// <summary>
        /// 取得模擬Sensor
        /// </summary>
        /// <param name="count">數量</param>
        /// <returns></returns>
        public SensorEntity CreateSimulateSensor();
    }
}