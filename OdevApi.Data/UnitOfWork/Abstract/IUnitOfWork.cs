namespace OdevApi.Data;

public interface IUnitOfWork : IDisposable
{
    void Complete();
}
