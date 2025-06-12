using MQTTLAB.gRPC.Controller;
using Sensor.Domain;

namespace Infrastructrue.APINotifier
{
  public class GrpcNotifier : IAPINotifier
  {
    private readonly SensorGrpc.SensorGrpcClient _client;

    public GrpcNotifier(SensorGrpc.SensorGrpcClient client)
    {
      _client = client;
    }

    public async Task<string> NotifySensorRegisterAsync(SensorEntity sensor)
    {
      var request = new SensorRegisterRequest()
      {
        Id = sensor.Id.ToString(),
        Type = (int)sensor.Type,
        Status = (int)sensor.Status,
        CreatedAt = sensor.createdAt,
        Topic = sensor.Topic
      };
      var response = await _client.RegisterAsync(request);
      return response.Message;
    }

    public async Task<string> UpdateStatusAsync(SensorEntity sensor)
    {
      var request = new SensorStatusUpdateRequest();
      request.Id = sensor.Id.ToString();
      request.Status = (int)sensor.Status;
      var response = await _client.UpdateStatusAsync(request);
      return response.Message;
    }
  }

}

