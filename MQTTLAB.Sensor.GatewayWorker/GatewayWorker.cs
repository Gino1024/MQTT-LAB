using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensor.AppService;

namespace Sensor.GateworkWorker;

public class GatewayWorker : BackgroundService
{
  private readonly ILogger<GatewayWorker> _logger;

  public GatewayWorker(ILogger<GatewayWorker> logger)
  {
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      Console.WriteLine("Running");
      await Task.Delay(2000);
    }
  }

  public override async Task StopAsync(CancellationToken stoppingToken)
  {
    //Dispose....

    await base.StopAsync(stoppingToken);
  }
}