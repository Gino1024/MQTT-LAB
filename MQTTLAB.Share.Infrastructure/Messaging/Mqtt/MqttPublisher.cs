using Sensor.Domain;
using MQTTnet;
using MQTTnet.Packets;
using System.Buffers;

namespace Infrastructrue;

public class MqttPublisher : IPublisher
{
    public async Task Publish(string topic, string msg)
    {  
        string brokerIP = "127.0.0.1";
        var mqttFactory = new MqttClientFactory();
        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(brokerIP, 1883)
                .WithTimeout(TimeSpan.FromSeconds(10))
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
                .Build();

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(msg)
                .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            await mqttClient.DisconnectAsync();

        }
    }
}