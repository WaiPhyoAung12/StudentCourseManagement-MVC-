using Mapster;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Constants;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.PageSetting;
using StudentCourseManagement.Models.Shared.ResultModel;
using StudentCourseManagement.Models.Student;
using StudentCourseManagement.Models.Students;
using StudentCourseManagement.Services.Shared;
using System.Runtime.CompilerServices;

namespace StudentCourseManagement.Services.Student
{
    public class StudentRepo
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Result<StudentListResponseModel> GetStudentListPagination(StudentListRequestModel studentListRequestModel)
        {
            var pageSettingModel = studentListRequestModel.PageSetting;
            var searchItem = pageSettingModel.SearchTerm;

              var query = _appDbContext.TblStudents.Where(x => x.DeleteFlag == false);

            if (!string.IsNullOrEmpty(searchItem))
            {
                query = query.Where(x => x.Name.Contains(searchItem)
            || x.Email.Contains(searchItem)
            || x.Phone.Contains(searchItem)
            || x.Address.Contains(searchItem));
            }
            int totalCount = query.ToList().Count;
            query = query.GetPagination(pageSettingModel);

            var datalist = query.ToList();

            if (datalist.Count <= 0)
                return Result<StudentListResponseModel>.Fail(ConstantMessages.DataNotFound);

            StudentListResponseModel studentList = new();
            studentList.StudentList = datalist.Adapt<List<StudentModel>>();
            studentList.TotalCount = totalCount;
            return Result<StudentListResponseModel>.Success(ConstantMessages.Success, studentList);
        }

        public async Task<Result<StudentModel>>CreateStudent(StudentRequestModel studentRequestModel)
        {
            TblStudent student = new TblStudent()
            {
                Name = studentRequestModel.Name,
                Address = studentRequestModel.Address,
                Email = studentRequestModel.Email,
                Phone = studentRequestModel.Phone,
                Gender = studentRequestModel.Gender,
                DeleteFlag = false
            };

            await _appDbContext.TblStudents.AddAsync(student);
            var result=await _appDbContext.SaveChangesAsync();

            return result<=0
                ?Result<StudentModel>.Fail(ConstantMessages.ErrorCreate)
                :Result<StudentModel>.Success(ConstantMessages.SuccessCreate);
        }

        public async Task<Result<StudentModel>>UpdateStudent(StudentRequestModel studentRequestModel)
        {
            var result = await _appDbContext.TblStudents.Where(x => x.DeleteFlag == false)
                .ExecuteUpdateAsync(x => x
                .SetProperty(s => s.Name, studentRequestModel.Name)
                .SetProperty(s => s.Address, studentRequestModel.Address)
                .SetProperty(s => s.Phone, studentRequestModel.Phone)
                .SetProperty(s => s.Email, studentRequestModel.Email)
                .SetProperty(s=>s.Gender, studentRequestModel.Gender)
                );

            return result <= 0
                ? Result<StudentModel>.Fail(ConstantMessages.ErrorUpdate)
                : Result<StudentModel>.Success(ConstantMessages.SuccessUpdate);
        }

        public async Task<Result<StudentModel>>DeleteStudent(int id)
        {
            var result=await _appDbContext.TblStudents.Where(x=>x.Id == id)
                .ExecuteUpdateAsync(x=>x.SetProperty(s=>s.DeleteFlag,true));

            return result <= 0
               ? Result<StudentModel>.Fail(ConstantMessages.ErrorDelete)
               : Result<StudentModel>.Success(ConstantMessages.SuccessDelete);
        }
    }
}
