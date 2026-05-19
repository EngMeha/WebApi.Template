using WebApi.Template.Application.Interfaces;

namespace WebApi.Template.Infrastructure.Data;

public class EfUnitOfWork: IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //TODO прописать реализацию
        throw new NotImplementedException();
    }
}