﻿using Dapper;
using System.Data;
using ToDoListAPI.Data;
using ToDoListAPI.Models.Queries;
using ToDoListAPI.Models.Responses;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Services.Implementations
{
    public class DapperService : IDapperService
    {
        private readonly DapperContext _context;

        public DapperService(DapperContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureData qData, DynamicParameters parameters, bool hasArray = false)
        {
            try
            {
                var spName = $"{qData.SchemaName}.{qData.Name}";

                using (IDbConnection connection = _context.CreateConnection(qData.IdConnectionString))
                {
                    dynamic? response = hasArray
                        ? await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure)
                       : await connection.QuerySingleOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);

                    return new ServiceResponse
                    {
                        Results = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }
    }
}
