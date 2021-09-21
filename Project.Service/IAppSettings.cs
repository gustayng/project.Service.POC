namespace Project.Service
{
    public interface IAppSettings
    {
        string[] AllowedOrigins { get; set; }
        string AssignmentUpdatedTopicName { get; set; }
        string dbConnectionString { get; set; }
        string SERVICE_BUS_CONNECTIONSTRING { get; set; }
        string APPINSIGHTS_CONNECTIONSTRING { get; set; }
    }
}