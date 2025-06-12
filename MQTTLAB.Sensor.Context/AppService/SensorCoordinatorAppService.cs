using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sensor.Domain;

namespace Sensor.AppService;

public class SensorCoordinatorAppService
{

  private readonly ILogger<SensorCoordinatorAppService> _logger;
  private ISensorFectory _sensorFectory;
  private ITopicResolve _topicResolve;
  private IAPINotifier _apiNotifier;
  private ISubscribe _subscribe;
  private SensorManager _sensorManager;
  private MessageHandler _messageHandler;
  private readonly ISensorRepository _sensorRepository;
  private readonly IUnitOfWork _unitOfWork;
  public SensorCoordinatorAppService(ISensorFectory sensorFectory, ITopicResolve topicResolve, IAPINotifier apiNotifier, SensorManager sensorManager, ILogger<SensorCoordinatorAppService> logger, MessageHandler messageHandler, ISensorRepository sensorRepository, IUnitOfWork unitOfWork, ISubscribe subscribe)
  {
    _logger = logger;
    _sensorFectory = sensorFectory;
    _apiNotifier = apiNotifier;
    _subscribe = subscribe;
    _topicResolve = topicResolve;

    _sensorManager = sensorManager;
    _messageHandler = messageHandler;

    _sensorRepository = sensorRepository;
    _unitOfWork = unitOfWork;
  }

  public void Simulation()
  {
    int count = 1;
    Parallel.ForEach(Enumerable.Range(0, count), (i) =>
    {
      var _ = Task.Run(async () =>
      {
        var sensor = _sensorFectory.CreateSimulateSensor();
        await _apiNotifier.NotifySensorRegisterAsync(sensor);
        sensor.Active();
        //更新Server
        await _apiNotifier.UpdateStatusAsync(sensor);
        //透過Manger執行傳送模擬資料
        while (true && sensor.Status == SensorStatus.Running)
        {
          await _sensorManager.SimulationAndPublish(sensor);
        }
      });
    });
  }

  public async Task SubscribeSimulation()
  {
    var activeOfSensors = await _sensorRepository.GetActiveSensors();
    await _subscribe.ConnectAsync();
    await _subscribe.SubscribeAsync("device/#", _messageHandler.ReceiveSensorDataEvent);
  }
}


