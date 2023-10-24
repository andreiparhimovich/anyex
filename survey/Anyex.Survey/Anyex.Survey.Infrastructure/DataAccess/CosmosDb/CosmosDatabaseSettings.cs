namespace Anyex.Survey.Infrastructure.DataAccess.CosmosDb;

public class CosmosDatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? SurveyContainerName { get; set; }
}