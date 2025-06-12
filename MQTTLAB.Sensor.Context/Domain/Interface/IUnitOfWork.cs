namespace Sensor.Domain
{
  public interface IUnitOfWork
  {
    /// <summary>
    /// 提交目前的資料庫交易變更。
    /// </summary>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
  }
}
