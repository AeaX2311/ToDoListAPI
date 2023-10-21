namespace ToDoListAPI.Models.Responses
{
    public class ServiceResponse
    {
        public bool HasError { get; set; }
        public string? Message { get; set; }
        public dynamic? Results { get; set; }
    }
}
