using StudentCourseManagement.Constants;
using StudentCourseManagement.Models.Shared.PageSetting;
using System.Security.Claims;
using System.Linq.Dynamic.Core;

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

    public static PageSettingModel GetPageSettingModel(this IFormCollection form)
    {
        int draw = Convert.ToInt32(form["draw"]);

        int pageSize = Convert.ToInt32(form["length"]);
        int skip = Convert.ToInt32(form["start"]);
        string? searchValue = form["search[value]"];

        string sortColumn = form[$"columns[{form["order[0][column]"]}][data]"];
        string sortDirection = form["order[0][dir]"];

        var pageSetting = new PageSettingModel
        {
            PageNumber = (skip / pageSize) + 1,
            PageSize = pageSize,
            SearchTerm = searchValue,
            SortColumn = sortColumn,
            SortDirection = sortDirection,
            Draw=draw,
        };
        return pageSetting;
     }

    public static IQueryable<T>GetPagination<T>(this IQueryable<T> query, PageSettingModel pageSettingModel)
    {
        var paginationQuery= query.Skip(pageSettingModel.SkipCount).Take(pageSettingModel.TakeCount);
        var sortColumn = string.IsNullOrWhiteSpace(pageSettingModel.SortColumn) ? "Id" : pageSettingModel.SortColumn;
        var sortDirection = pageSettingModel.SortDirection == null ? "asc" : pageSettingModel.SortDirection.ToLower();

        var sort = $"{sortColumn} {sortDirection}";

        query = paginationQuery.OrderBy(sort);
        return query;
    }

}
