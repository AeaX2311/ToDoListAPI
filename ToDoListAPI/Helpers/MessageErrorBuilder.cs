using ToDoListAPI.Models.Responses;

namespace ToDoListAPI.Helpers {
    public static class MessageErrorBuilder
    {
        public static GenericResponseData GenerateError(string innerException)
        {
            return new GenericResponseData
            {
                Type = "danger",
                Title = "Error",
                Message = "Error inesperado, favor de intentarlo nuevamente en unos minutos.",
                InnerException = innerException
            };
        }
    }
}
