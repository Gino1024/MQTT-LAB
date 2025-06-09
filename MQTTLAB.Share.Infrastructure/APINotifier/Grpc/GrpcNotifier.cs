using MQTTLAB.gRPC.Controller;
using Sensor.Domain;

namespace Infrastructrue.APINotifier;

public class GrpcNotifier : IAPINotifier
{
  private readonly Greeter.GreeterClient _client;

  public GrpcNotifier(Greeter.GreeterClient client)
  {
    _client = client;
  }

  // public async Task NotifySensorActivatedAsync(Sensor sensor)
  // {
  //   var request = new SensorActivatedRequest { Id = sensor.Id.ToString() };
  //   await _client.NotifyActivatedAsync(request);
  // }

  public async Task NotifySensorRegisterAsync(SensorEntity sensor)
  {
    await _client.SayHelloAsync(new HelloRequest() { Name = $"ID: {sensor.Id}, Type:{sensor.Type}" });
  }
}
