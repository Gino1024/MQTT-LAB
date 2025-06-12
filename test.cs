using System.Buffers;
using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Packets;

string topic = "device/abc123/notify";
string brokerIP = "127.0.0.1";


var publish = async (CancellationToken cts) =>
{
    var mqttFactory = new MqttClientFactory();

    using (var mqttClient = mqttFactory.CreateMqttClient())
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIP, 1883)
            .WithTimeout(TimeSpan.FromSeconds(10))
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
            .Build();

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        int i = 0;
        while (true && !cts.IsCancellationRequested)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload("Hello world " + i.ToString())
                .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            //await mqttClient.DisconnectAsync();

            Console.WriteLine("MQTT application message is published.");
            i++;
            await Task.Delay(1000);
        }
    }
};


var subscribe = async (CancellationToken cts) =>
{
    var mqttFactory = new MqttClientFactory();
    using (var mqttClient = mqttFactory.CreateMqttClient())
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIP, 1883)
            .WithTimeout(TimeSpan.FromSeconds(10))
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
            .Build();

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = e.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(e.ApplicationMessage?.Payload.ToArray() ?? new byte[0]);
            //handler(topic, payload);

            Console.WriteLine($"Received application message: {payload}");
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

        Console.WriteLine("MQTT client subscribed to topic ");
        while (true && !cts.IsCancellationRequested) ;
    }
};

CancellationTokenSource cts1 = new CancellationTokenSource();
subscribe(cts1.Token);
await Task.Delay(500);
CancellationTokenSource cts2 = new CancellationTokenSource();
publish(cts2.Token);

await Task.Delay(10000);
cts2.Cancel();
cts1.Cancel();
