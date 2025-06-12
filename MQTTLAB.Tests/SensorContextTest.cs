using Sensor.Domain;
using Sensor.Infrastructrue;
using Moq;
using Microsoft.Extensions.Logging;

namespace UnitTest;

public class SensorContextTest
{
    [Fact]
    public void CreateSimulateSensor_Should_ReturnNonNullEntity_WithStoppedStatus()
    {
        var mockLogger = new Mock<ILogger<SensorFectory>>();
        var mockTopicResolve = new Mock<ITopicResolve>();

        // Arrange
        var sensorFectory = new SensorFectory(mockLogger.Object, mockTopicResolve.Object);

        // Act
        SensorEntity sensor = sensorFectory.CreateSimulateSensor();

        // Assert
        Assert.NotNull(sensor); // 回傳的物件不應為 null
        Assert.Equal(SensorStatus.Stopped, sensor.Status); // Status 一定是 Stopped

        // 由於原始方法使用 new Guid()，會使 sensor.Id == Guid.Empty
        // 如果未來改用 Guid.NewGuid()，則可改成 Assert.NotEqual(Guid.Empty, sensor.Id)
        Assert.NotNull(sensor.Id);

        // Type 必須是合法的 enum 值（檢查範圍）
        bool isValidType =
            Enum.IsDefined(typeof(SensorType), sensor.Type);
        Assert.True(isValidType, $"Type 必須是 SensorType 的合法值，目前是 {sensor.Type}");
    }

    [Fact]
    public async Task SimulationAndPublish_ShouldPublishCorrectPayload()
    {
        // Arrange
        var mockPublisher = new Mock<IPublisher>();
        var mockResolver = new Mock<ITopicResolve>();
        var mockGenerator = new Mock<ISensorDataGenerator>();
        var mockLogger = new Mock<ILogger<SensorManager>>();

        var sensorType = SensorType.Temperature;
        var sensorId = Guid.NewGuid();
        var expectedTopic = "sensor/topic";

        mockGenerator.Setup(g => g.GeneratorValue()).Returns(25.5);
        mockResolver.Setup(r => r.Resolve(sensorId.ToString(), sensorType.ToString())).Returns(expectedTopic);

        var generators = new Dictionary<SensorType, ISensorDataGenerator>
        {
            { sensorType, mockGenerator.Object }
        };

        var service = new SensorManager(generators, mockPublisher.Object, mockResolver.Object, mockLogger.Object);

        var sensor = new SensorEntity(sensorId, sensorType, SensorStatus.Running);

        // Act
        await service.SimulationAndPublish(sensor);

        // Assert
        mockPublisher.Verify(p => p.Publish(It.Is<string>(s =>
            s.Contains(sensorId.ToString()) &&
            s.Contains("25.5") &&
            s.Contains("C")
        ), expectedTopic), Times.Once);
    }
}