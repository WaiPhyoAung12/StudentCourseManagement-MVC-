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

    public async Task<Result<BatchModel>> Delete(int id)
    {
        var result=await _batchRepo.DeleteBatch(id);
        return result;
    }

    public async Task<Result<BatchListResponseModel>> GetBatchListPagination(BatchListPaginationModel batchListPagination)
    {
        var result=await _batchRepo.GetBatchListPagination(batchListPagination);
        return result;
    }

    public async Task<Result<BatchResponseModel>> GetBatchById(int id)
    {
        var result=await _batchRepo.GetBatchById(id);
        return result;
    }

    public async Task<Result<BatchModel>> UpdateBatch(BatchRequestModel batchRequestModel)
    {
        var result=await _batchRepo.UpdateBatch(batchRequestModel);
        return result;
    }
}
