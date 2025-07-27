using StudentCourseManagement.AppDbContextModels;

namespace StudentCourseManagement.Services.RoleFunction;

public class RoleFunctionRepo
{
    private readonly AppDbContext _appDbContext;

    public RoleFunctionRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CheckPermission(string functionCode)
    {

    }
}
