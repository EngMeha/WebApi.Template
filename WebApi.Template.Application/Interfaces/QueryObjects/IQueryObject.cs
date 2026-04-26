namespace WebApi.Template.Application.Interfaces.QueryObjects;

/// <summary>
/// Универсальный интерфейс получения данных из БД
/// </summary>
/// <typeparam name="TQuery">Объект запроса</typeparam>
/// <typeparam name="TResult">Объект Результата</typeparam>
public interface IQueryObject<in TQuery, TResult>: IQueryMarker
{
    public Task<TResult> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
}