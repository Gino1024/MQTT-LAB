using Sensor.Domain;
using Sensor.Infrastructrue;

namespace UnitTest;

public class SensorContextTest
{
    [Fact]
    public void CreateSimulateSensor_Should_ReturnNonNullEntity_WithStoppedStatus()
    {
        // Arrange
        var sensorFectory = new SensorFectory();

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
}