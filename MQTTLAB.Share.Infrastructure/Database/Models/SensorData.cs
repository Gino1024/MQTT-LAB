using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Database;

public class SensorData
{
  public Guid id { get; set; }
  public Guid sensor_id { get; set; }
  public int value { get; set; }
  public int status { get; set; }
  public long created_at { get; set; }
  public virtual Sensor sensor { get; set; }
}