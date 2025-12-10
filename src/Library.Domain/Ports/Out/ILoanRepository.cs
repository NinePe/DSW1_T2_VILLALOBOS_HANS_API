using Library.Domain.Entities;

namespace Library.Domain.Ports.Out;

public interface ILoanRepository : IRepository<Loan>
{
    // Listar todos los préstamos activos
    Task<IEnumerable<Loan>> GetActiveLoansAsync();

    // (Opcional) si quieres conservar el filtro por libro
    Task<IEnumerable<Loan>> GetActiveLoansByBookIdAsync(int bookId);
}
