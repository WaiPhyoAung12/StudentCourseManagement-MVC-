using Mapster;
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

    public async Task<Result<BatchListResponseModel>> GetCourse(BatchListPaginationModel batchListPagination)
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
                        BatchName=b.Title,
                        Description=b.Description,
                        CreditHour=b.CreditHour,
                        Capacity=b.Capacity,
                        CourseName=c.CourseTitle,
                        CreatedDateTime=b.CreatedDateTime,                           
                    };

            _appDbContext.TblBatches.Where(x => x.DeleteFlag == false);

        if (!string.IsNullOrEmpty(searchItem))
        {
            query = query.Where(x => x.DeleteFlag != true && x.BatchName.Contains(searchItem)
        || x.Description.Contains(searchItem)
        || x.Capacity.ToString().Contains(searchItem)
        || x.CreditHour.ToString().Contains(searchItem)
        || x.CourseName.Contains(searchItem)
        || x.CreatedDateTime.ToString().Contains(searchItem)
        );
        }
        int totalCount = query.ToList().Count;
        query = query.GetPagination(pageSettingModel);

        var datalist = query.ToList();

        if (datalist.Count <= 0)
            return Result<BatchListResponseModel>.Fail(ConstantMessages.DataNotFound);

        CourseListResponseModel courseList = new();
        courseList.CourseModels = datalist.Adapt<List<CourseModel>>();
        courseList.TotalCount = totalCount;
        return Result<BatchListResponseModel>.Success(ConstantMessages.Success, courseList);
    }
}
