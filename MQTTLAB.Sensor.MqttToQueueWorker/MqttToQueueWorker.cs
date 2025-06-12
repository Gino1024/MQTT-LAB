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
  private readonly SensorCoordinatorAppService _sensorCoordinatorAppService;

  public GatewayWorker(ILogger<GatewayWorker> logger, SensorCoordinatorAppService sensorCoordinatorAppService)
  {
    _sensorCoordinatorAppService = sensorCoordinatorAppService;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await _sensorCoordinatorAppService.SubscribeSimulation();
  }

  public override async Task StopAsync(CancellationToken stoppingToken)
  {
    //Dispose....

    await base.StopAsync(stoppingToken);
  }
}