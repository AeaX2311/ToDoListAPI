namespace ToDoListAPI.Models.Responses
{
    public class GenericResponseData
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string InnerException { get; set; } = string.Empty;
    }
}
