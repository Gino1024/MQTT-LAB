using Sensor.Domain;
using MQTTnet;
using MQTTnet.Packets;
using System.Buffers;
using Microsoft.VisualBasic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructrue.Messaging.Mqtt;

public class MqttSubscribe : ISubscribe
{
  private readonly string brokerIP = "127.0.0.1";
  private readonly MqttClientFactory mqttFactory = new MqttClientFactory();
  private readonly IMqttClient mqttClient;
  private readonly ILogger<MqttSubscribe> _logger;
  public MqttSubscribe(ILogger<MqttSubscribe> logger)
  {
    mqttClient = mqttFactory.CreateMqttClient();
    _logger = logger;
  }

  public async Task ConnectAsync()
  {
    _logger.LogInformation($"Connect Async...");
    var mqttClientOptions = new MqttClientOptionsBuilder()
           .WithTcpServer(brokerIP, 1883)
           .WithTimeout(TimeSpan.FromSeconds(10))
           .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
           .Build();

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
  }

  public async Task SubscribeAsync(string topic, Func<string, Task> handler)
  {
    _logger.LogInformation($"SubscribeAsync topic: {topic}");

    mqttClient.ApplicationMessageReceivedAsync += e =>
        {
          var topic = e.ApplicationMessage.Topic;
          var payload = e.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(e.ApplicationMessage?.Payload.ToArray() ?? new byte[0]);
          _logger.LogInformation($"Receive Data. {payload}");
          handler(payload);

          return Task.CompletedTask;
        };

    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                                .WithTopicFilter(
                                    f =>
                                    {
                                      f.WithTopic(topic);
                                    })
                                .Build();

    var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

    _logger.LogInformation($"MQTT client subscribed");
    while (true) ;
  }
}
