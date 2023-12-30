namespace Myafim.Domain.Repositories;

public interface IUnitOfWork
{
    Task WithTransactionAsync(Func<Task> funcAsync, CancellationToken cancellationToken = default);
    Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> funcAsync, CancellationToken cancellationToken = default);
}