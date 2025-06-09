using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Database;

public class Sensor
{
  public int id { get; set; }
  public Guid key { get; set; }
  public int type { get; set; }
  public int status { get; set; }
  public long createdAt { get; set; }
}