using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Models.Shared.ResultModel;

namespace StudentCourseManagement.Services.Batch;

public class BatchService : IBatchService
{
    private readonly BatchRepo _batchRepo;

    public BatchService(BatchRepo batchRepo)
    {
        _batchRepo = batchRepo;
    }
    public async Task<Result<BatchModel>> CreateBatch(BatchRequestModel batchRequest)
    {
        var result=await _batchRepo.CreateBatch(batchRequest);
        return result;
    }
}
