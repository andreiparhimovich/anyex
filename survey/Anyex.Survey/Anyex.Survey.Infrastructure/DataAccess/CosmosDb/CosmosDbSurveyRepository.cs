using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Infrastructure.DataAccess.CosmosDb.DbModels;
using Microsoft.Azure.Cosmos;

namespace Anyex.Survey.Infrastructure.DataAccess.CosmosDb;

public class CosmosDbSurveyRepository : IRepository<Domain.Survey, string>
{
    private readonly Container _container;

    public CosmosDbSurveyRepository(CosmosClient cosmosClient, CosmosDatabaseSettings settings)
    {
        _container = cosmosClient.GetDatabase(settings.DatabaseName).GetContainer(settings.SurveyContainerName);
    }

    public async Task<Domain.Survey?> GetByIdAsync(string id)
    {
        var surveyDbModel = 
            await _container.ReadItemAsync<SurveyDbModel>(id: id, partitionKey: new PartitionKey(id));

        return DbModelsConverter.ToDomainModel(surveyDbModel);
    }

    public async Task<Domain.Survey> CreateAsync(Domain.Survey survey)
    {
        var dbModel = DbModelsConverter.ToDbModel(survey);

        await _container.CreateItemAsync(dbModel, partitionKey: new PartitionKey(dbModel.Id));

        return survey;
    }

    public async Task UpdateAsync(Domain.Survey survey)
    {
        var dbModel = DbModelsConverter.ToDbModel(survey);

        await _container.ReplaceItemAsync(dbModel, id: dbModel.Id, partitionKey: new PartitionKey(dbModel.Id));
    }
}