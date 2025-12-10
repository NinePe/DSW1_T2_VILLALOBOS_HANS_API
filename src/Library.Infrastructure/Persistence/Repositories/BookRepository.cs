using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Book?> GetByIsbnAsync(string isbn) =>
        await _dbSet.FirstOrDefaultAsync(b => b.ISBN == isbn);
}
