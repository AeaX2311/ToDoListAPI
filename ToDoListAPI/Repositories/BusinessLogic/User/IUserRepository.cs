using ToDoListAPI.Models.BusinessLogic;
using ToDoListAPI.Models.Responses;

namespace ToDoListAPI.Repositories.BusinessLogic.User {
    public interface IUserRepository {
        Task<GenericResponse<GenericCrud>> UserRegistrationAsync(UserRegistration user);
        Task<GenericResponse<UserLoginResponse>> UserValidationAsync(UserValidation user);
    }
}
