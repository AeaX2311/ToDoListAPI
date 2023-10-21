using Dapper;
using ToDoListAPI.Models.Queries;
using ToDoListAPI.Models.Responses;

namespace ToDoListAPI.Services.Interface
{
    public interface IDapperService
    {
        Task<ServiceResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureData qData, DynamicParameters parameters, bool hasArray = false);
    }
}
