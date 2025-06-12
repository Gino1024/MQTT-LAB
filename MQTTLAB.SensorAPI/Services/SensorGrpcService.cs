using Grpc.Core;
using MQTTLAB.gRPC.Controller;
using Sensor.Domain;

namespace MQTTLAB.gRPC.Controller.Services;

public class SensorGrpcService : SensorGrpc.SensorGrpcBase
{
    private readonly ILogger<SensorGrpcService> _logger;
    private readonly ISensorRepository _sensorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public SensorGrpcService(ILogger<SensorGrpcService> logger, ISensorRepository sensorRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _sensorRepository = sensorRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<BaseResponse> Register(SensorRegisterRequest request, ServerCallContext context)
    {
        try
        {
            var result = new BaseResponse();

            if (!Guid.TryParse(request.Id, out var id))
            {
                result.Message = "失敗：ID有誤";
                return result;
            }


            SensorEntity sensorEntity = new SensorEntity(id, (SensorType)request.Type, (SensorStatus)request.Status);
            sensorEntity.createdAt = request.CreatedAt;
            sensorEntity.Topic = request.Topic;
            await _sensorRepository.Save(sensorEntity);
            var isSuccess = await _unitOfWork.CommitAsync() > 0;
            result.Message = (isSuccess) ? "註冊成功" : "未預期錯誤";
            return result;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public override async Task<BaseResponse> UpdateStatus(SensorStatusUpdateRequest request, ServerCallContext context)
    {
        try
        {
            var result = new BaseResponse();

            if (!Guid.TryParse(request.Id, out var id))
            {
                result.Message = "失敗：Id有誤";
                return result;
            }

            SensorEntity sensor = new SensorEntity(id, 0, (SensorStatus)request.Status);

            _sensorRepository.UpdateStatus(sensor);
            var isSuccess = await _unitOfWork.CommitAsync() > 0;
            result.Message = (isSuccess) ? "更新成功" : "未預期錯誤";
            return result;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
