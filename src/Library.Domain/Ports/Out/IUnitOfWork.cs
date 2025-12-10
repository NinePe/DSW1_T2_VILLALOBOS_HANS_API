namespace Library.Domain.Ports.Out;

public interface IUnitOfWork : IAsyncDisposable
{
    IBookRepository Books { get; }
    ILoanRepository Loans { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}