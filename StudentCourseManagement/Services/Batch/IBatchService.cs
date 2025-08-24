using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Models.Shared.ResultModel;

namespace StudentCourseManagement.Services.Batch;

public interface IBatchService
{
    Task<Result<BatchModel>> CreateBatch(BatchRequestModel batchRequest);
    Task<Result<BatchListResponseModel>> GetBatchListPagination(BatchListPaginationModel batchListPagination);
    Task<Result<BatchResponseModel>> GetBatchById(int id);
    Task<Result<BatchModel>> UpdateBatch(BatchRequestModel batchRequestModel);
    Task<Result<BatchModel>> Delete(int id);
}
