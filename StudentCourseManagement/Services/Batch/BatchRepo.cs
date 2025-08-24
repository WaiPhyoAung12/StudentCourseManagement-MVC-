using Mapster;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Constants;
using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;
using StudentCourseManagement.Services.Shared;

namespace StudentCourseManagement.Services.Batch;

public class BatchRepo
{
    private readonly AppDbContext _appDbContext;

    public BatchRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<BatchModel>> CreateBatch(BatchRequestModel batchRequest)
    {
        TblBatch tblBatch = new();
        tblBatch = batchRequest.Adapt<TblBatch>();
        tblBatch.CreatedUserId = "testing";
        tblBatch.CreatedDateTime=DateTime.Now;

        await _appDbContext.AddAsync(tblBatch);
        var result=await _appDbContext.SaveChangesAsync();

        return result > 0
            ? Result<BatchModel>.Success(ConstantMessages.SuccessCreate)
            : Result<BatchModel>.Fail(ConstantMessages.ErrorCreate);
    }

    public async Task<Result<BatchListResponseModel>> GetBatchListPagination(BatchListPaginationModel batchListPagination)
    {
        var pageSettingModel = batchListPagination.PageSetting;
        var searchItem = pageSettingModel.SearchTerm;

        var query = from b in _appDbContext.TblBatches
                    join c in _appDbContext.TblCourses
                    on b.CourseId equals c.Id
                    where b.DeleteFlag == false && c.DeleteFlag == false
                    select new BatchModel
                    {
                        Id=b.Id,
                        Title=b.Title,
                        Description=b.Description,
                        CreditHour=b.CreditHour,
                        Capacity=b.Capacity,
                        CourseName=c.CourseTitle,
                        CreatedDateTime=b.CreatedDateTime,                           
                    };

            //_appDbContext.TblBatches.Where(x => x.DeleteFlag == false);

        if (!string.IsNullOrEmpty(searchItem))
        {
            query = query.Where(x => x.DeleteFlag != true && x.Title.Contains(searchItem)
        || x.Description.Contains(searchItem)
        || x.Capacity.ToString().Contains(searchItem)
        || x.CreditHour.ToString().Contains(searchItem)
        || x.CourseName.Contains(searchItem)
        || x.CreatedDateTime.ToString().Contains(searchItem)
        );
        }
        int totalCount = query.ToList().Count;
        try
        {
            query = query.GetPagination(pageSettingModel);

            var datalist = query.ToList();

            if (datalist.Count <= 0)
                return Result<BatchListResponseModel>.Fail(ConstantMessages.DataNotFound);

            BatchListResponseModel batchList = new();
            batchList.BatchModels = datalist.Adapt<List<BatchModel>>();
            batchList.TotalCount = totalCount;
            return Result<BatchListResponseModel>.Success(ConstantMessages.Success, batchList);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<Result<BatchResponseModel>>GetBatchById(int id)
    {

        var batch = (from b in _appDbContext.TblBatches
                     join c in _appDbContext.TblCourses
                     on b.CourseId equals c.Id
                     where b.DeleteFlag == false && c.DeleteFlag == false
                     && b.Id == id
                     select new BatchResponseModel
                     {
                         Title = b.Title,
                         Description = b.Description,
                         CreditHour = b.CreditHour,
                         Capacity = b.Capacity,
                         CourseId = c.Id,
                         CourseTitle = c.CourseTitle,
                     }).FirstOrDefault();
            
            
        if(batch is null)
            return Result<BatchResponseModel>.Fail(ConstantMessages.DataNotFound);

        BatchResponseModel batchResponseModel = new();
        batchResponseModel = batch.Adapt<BatchResponseModel>();

        return Result<BatchResponseModel>.Success(ConstantMessages.Success,batchResponseModel);
    }

    public async Task<Result<BatchModel>>UpdateBatch(BatchRequestModel batchRequestModel)
    {
        var result=await _appDbContext.TblBatches
            .Where(x=>x.Id==batchRequestModel.Id && x.DeleteFlag==false)
            .ExecuteUpdateAsync(x=>x
            .SetProperty(b=>b.Description, batchRequestModel.Description)
            .SetProperty(b => b.Title, batchRequestModel.Title)
            .SetProperty(b => b.CourseId, batchRequestModel.CourseId)
            .SetProperty(b => b.CreditHour, batchRequestModel.CreditHour)
            .SetProperty(b => b.Capacity, batchRequestModel.Capacity)
            .SetProperty(b => b.UpdatedDateTime,DateTime.Now)
            .SetProperty(b => b.UpdatedUserId, Guid.NewGuid().ToString())
            );

        return result>0
            ?Result<BatchModel>.Success(ConstantMessages.SuccessUpdate) 
            :Result<BatchModel>.Fail(ConstantMessages.ErrorUpdate);
    }

    public async Task<Result<BatchModel>>DeleteBatch(int id)
    {
        var result = await _appDbContext.TblBatches
            .Where(x => x.Id == id && x.DeleteFlag == false)
            .ExecuteUpdateAsync(x => x.SetProperty(b => b.DeleteFlag, true));

        return result > 0
            ? Result<BatchModel>.Success(ConstantMessages.SuccessUpdate)
            : Result<BatchModel>.Fail(ConstantMessages.ErrorUpdate);
    }
}
