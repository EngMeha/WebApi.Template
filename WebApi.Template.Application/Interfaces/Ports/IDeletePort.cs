namespace WebApi.Template.Application.Interfaces.Ports;

public interface IDeletePort<in TEntity>: IPortMarker where TEntity: class
{
    public void Delete(TEntity entity);
}