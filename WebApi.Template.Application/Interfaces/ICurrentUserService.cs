namespace WebApi.Template.Application.Interfaces;

public interface ICurrentUserService
{
    public Guid UserId { get; }
}