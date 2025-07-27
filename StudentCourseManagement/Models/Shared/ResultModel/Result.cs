namespace StudentCourseManagement.Models.Shared.ResultModel;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public T? Data { get; set; }
    public string Message { get; set; }
    public List<string> MessageList { get; set; }
    public static Result<T> Success(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
        };
    }

    public static Result<T>Fail(string message)
    {
        return new Result<T>
        {
            IsSuccess=false,
            Message = message,
        };
    }

    public static Result<T>FailValidation(List<string>? MessageList)
    {
        return new Result<T>
        {
            IsSuccess = false,
            MessageList = MessageList.ToList(),
        };
    }
}
