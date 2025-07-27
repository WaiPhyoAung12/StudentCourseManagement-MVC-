using StudentCourseManagement.Constants;
using System.Security.Claims;

namespace StudentCourseManagement.Services.Shared;

public static class DevCode
{
    public static IHttpContextAccessor HttpContextAccessor { get; set; }

    public static int GetRole()
    {
        var user = HttpContextAccessor.HttpContext!.User;
        var role = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.Role)?.Value;
        if (role is null)
            return 0;

        return role.ToInt();

    }
    public static string GetUserId()
    {
        var user = HttpContextAccessor.HttpContext!.User;
        var userId = user.Claims.FirstOrDefault(c => c.Type == ConstantClaims.UserId)?.Value;
        if (userId is null)
            return null;

        return userId;

    }
    public static int ToInt(this object value)
    {
        return Convert.ToInt32(value);
    }
}
