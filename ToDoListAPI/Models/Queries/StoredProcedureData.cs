namespace ToDoListAPI.Models.Queries
{
    public class StoredProcedureData
    {
        public StoredProcedureData(string schemaName, string name, string idConnectionString)
        {
            SchemaName = schemaName;
            Name = name;
            IdConnectionString = idConnectionString;
        }

        public string SchemaName { get; set; }
        
        public string Name { get; set; }
        
        public string IdConnectionString { get; set; }
    }
}