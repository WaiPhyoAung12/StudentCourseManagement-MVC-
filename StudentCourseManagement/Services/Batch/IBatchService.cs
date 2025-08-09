using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Models.Shared.ResultModel;

namespace StudentCourseManagement.Services.Batch;

public interface IBatchService
{
    Task<Result<BatchModel>> CreateBatch(BatchRequestModel batchRequest);
}
