namespace Api.SettingsModels 
{    
    public class DemoDatabaseSettings : IDemoDatabaseSettings
    {
        public string DemoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDemoDatabaseSettings
    {
        string DemoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}