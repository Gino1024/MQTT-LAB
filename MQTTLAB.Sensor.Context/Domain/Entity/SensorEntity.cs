namespace Sensor.Domain
{
    public class SensorEntity
    {
        public SensorEntity()
        {
        }
        public SensorEntity(Guid id, SensorType type, SensorStatus status)
        {
            this.Id = id;
            this.Type = type;
            this.Status = status;
        }
        public Guid Id { get; set; }
        public SensorType? Type { get; set; }
        public SensorStatus? Status { get; set; }
        public long createdAt { get; set; }
        public string Topic { get; set; }

        public void Active() => this.Status = SensorStatus.Running;
        public void Deactive() => this.Status = SensorStatus.Stopped;
    }
}