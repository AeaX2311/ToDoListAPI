namespace ToDoListAPI.Models.Responses
{
    public class GenericResponse<T>
    {
        public int StatusCode { get; set; }
        public T? Content { get; set; }
        public GenericResponseData? Message { get; set; }
        public string? Token { get; set; }
    }
}
