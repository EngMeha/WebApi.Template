using WebApi.Template.Application.Interfaces;

namespace WebApi.Template.Infrastructure.Data;

public class EfUnitOfWork: IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //TODO прописать реализацию
        throw new NotImplementedException();
    }
}