namespace ExpeditionService.DataAccess.Models
{
    public class ExpeditionServiceMongoDbSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; } 
    }
}
