namespace MyProject.SharedLibrary.Responses
{
    public record Response<TData>(bool IsSuccess, string Message, TData? Data = default);


}
