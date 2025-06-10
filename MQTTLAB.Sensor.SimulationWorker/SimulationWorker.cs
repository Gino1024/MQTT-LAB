using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensor.AppService;

namespace Sensor.Worker
{
    public class SimulationWorker : BackgroundService
    {
        private readonly ILogger<SimulationWorker> _logger;
        private readonly SensorCoordinatorAppService _sensorCoordinatorAppService;

        public SimulationWorker(ILogger<SimulationWorker> logger, SensorCoordinatorAppService sensorCoordinatorAppService)
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
