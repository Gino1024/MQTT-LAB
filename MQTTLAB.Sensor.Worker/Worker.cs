using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensor.AppService;

namespace Sensor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SensorCoordinatorAppService _sensorCoordinatorAppService;

        public Worker(ILogger<Worker> logger, SensorCoordinatorAppService sensorCoordinatorAppService)
        {
            _logger = logger;
            _sensorCoordinatorAppService = sensorCoordinatorAppService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _sensorCoordinatorAppService.Simulation();
        }
    }
}
