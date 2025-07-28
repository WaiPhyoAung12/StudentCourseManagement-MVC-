namespace StudentCourseManagement.Models.Shared.PageSetting;

public class PageSettingModel
{
    public int PageNumber { get; set; }     // Current page (1-based)
    public int PageSize { get; set; }        // Items per page

    public string? SortColumn { get; set; }    // Column name to sort by
    public string SortDirection { get; set; } // "asc" or "desc"
    public string? SearchTerm { get; set; }  // Optional filter
    public int SkipCount => (PageNumber - 1) * PageSize;
    public int TakeCount => PageSize;

    public int TotalCount { get; set; }
}
