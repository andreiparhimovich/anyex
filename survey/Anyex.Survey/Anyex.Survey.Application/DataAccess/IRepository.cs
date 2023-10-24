namespace Anyex.Survey.Application.DataAccess;

public interface IRepository<TModel, TKey>
{
    Task<TModel?> GetByIdAsync(TKey id);
    Task<TModel> CreateAsync(TModel model);
    Task UpdateAsync(TModel model);
}