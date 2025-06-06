using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensor.Domain;

namespace Sensor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SensorDataSimulationService _sensorDataSimulationService;

        public Worker(ILogger<Worker> logger, SensorDataSimulationService sensorDataSimulationService)
        {
            _logger = logger;
            _sensorDataSimulationService = sensorDataSimulationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _sensorDataSimulationService.SimulationAndPublish();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
