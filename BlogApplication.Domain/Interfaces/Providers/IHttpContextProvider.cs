namespace BlogApplication.Domain.Interfaces.Providers
{
    public interface IHttpContextProvider
    {
        string UserId { get; }
    }
}