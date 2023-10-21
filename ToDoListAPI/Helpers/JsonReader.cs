using ToDoListAPI.Models.Queries;

namespace ToDoListAPI.Helpers
{
    public static class JsonReader
    {
        public static StoredProcedureData GetConfigurationStoredProcedure(IConfiguration configuration, string repositoryKey)
        {
            return new(configuration[$"{repositoryKey}:schemaName"], configuration[$"{repositoryKey}:spName"], configuration[$"{repositoryKey}:connectionId"]);
        }

    }
}
