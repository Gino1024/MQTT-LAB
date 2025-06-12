using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Sensor.Domain;

public class MessageHandler
{
  private readonly ILogger<MessageHandler> _logger;
  private readonly ISensorDataRepository _sensorDataRepository;
  private readonly IUnitOfWork _unitOfWork;
  public MessageHandler(ILogger<MessageHandler> logger, ISensorDataRepository sensorDataRepository, IUnitOfWork unitOfWork)
  {
    _logger = logger;
    _sensorDataRepository = sensorDataRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task ReceiveSensorDataEvent(string payload)
  {
    SensorDataEntity entity = SensorDataEntity.FromMqtt(payload);
    _logger.LogInformation("info: " + System.Text.Json.JsonSerializer.Serialize(entity));
    await _sensorDataRepository.Save(entity);
    await _unitOfWork.CommitAsync();
  }
}