namespace Infrastructrue.Database;

public class Sensor
{
  public Guid id { get; set; }
  public int type { get; set; }
  public int status { get; set; }
  public string topic { get; set; }
  public long created_at { get; set; }
  public virtual ICollection<SensorData> SensorDatas { get; set; }
}