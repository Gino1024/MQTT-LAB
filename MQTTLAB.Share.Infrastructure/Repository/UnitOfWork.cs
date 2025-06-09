using Infrastructrue.Database;
using Sensor.Domain;

namespace Infrastructrue.Repository;

public class UnitOfWork : IUnitOfWork
{
  public readonly MQTTLABDbContext _mqttlabDbContext;

  public UnitOfWork(MQTTLABDbContext mqttlabDbContext)
  {
    _mqttlabDbContext = mqttlabDbContext;
  }

  public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
  {
    return await _mqttlabDbContext.SaveChangesAsync();
  }
}
