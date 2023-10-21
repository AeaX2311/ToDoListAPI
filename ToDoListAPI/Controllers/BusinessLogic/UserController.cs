using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ToDoListAPI.Helpers;
using ToDoListAPI.Models.BusinessLogic;
using ToDoListAPI.Models.Responses;
using ToDoListAPI.Repositories.BusinessLogic.User;

namespace ToDoListAPI.Controllers.BusinessLogic
{
    [ApiController]
    [Route("todolistapi/users")]
    [ApiExplorerSettings(GroupName = "Users")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly GenericResponse<JObject> _error = new();

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistration user)
        {
            try
            {
                var result = await _userRepository.UserRegistrationAsync(user);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _error.StatusCode = 500;
                _error.Message = MessageErrorBuilder.GenerateError(ex.Message);
                return StatusCode(_error.StatusCode, _error);
            }
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> UserLogIn([FromBody] UserValidation user)
        {
            try
            {
                var result = await _userRepository.UserValidationAsync(user);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _error.StatusCode = 500;
                _error.Message = MessageErrorBuilder.GenerateError(ex.Message);
                return StatusCode(_error.StatusCode, _error);
            }
        }
    }
}
