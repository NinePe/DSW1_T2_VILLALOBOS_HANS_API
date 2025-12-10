using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence;

public class LoanRepository : Repository<Loan>, ILoanRepository
{
    public LoanRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Loan>> GetActiveLoansAsync() =>
        await _dbSet
            .Include(l => l.Book)
            .Where(l => l.Status == "Active")
            .ToListAsync();

    public async Task<IEnumerable<Loan>> GetActiveLoansByBookIdAsync(int bookId) =>
        await _dbSet
            .Include(l => l.Book)
            .Where(l => l.BookId == bookId && l.Status == "Active")
            .ToListAsync();
}
