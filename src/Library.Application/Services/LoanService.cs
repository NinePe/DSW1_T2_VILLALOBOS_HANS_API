using AutoMapper;
using Library.Application.DTOs.Loan;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;

namespace Library.Application.Services;

public class LoanService : ILoanService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public LoanService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    // 1) No se puede prestar si Stock = 0
    // 2) Al prestar: Stock--
    public async Task<LoanDto> CreateLoanAsync(CreateLoanDto dto)
    {
        var book = await _uow.Books.GetByIdAsync(dto.BookId)
                   ?? throw new InvalidOperationException("Libro no encontrado.");

        if (book.Stock <= 0)
            throw new InvalidOperationException("No hay stock disponible para este libro.");

        var loan = new Loan
        {
            BookId = dto.BookId,
            StudentName = dto.StudentName,
            LoanDate = DateTime.UtcNow,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Loans.AddAsync(loan);
        book.Stock -= 1;

        await _uow.SaveChangesAsync();

        var result = _mapper.Map<LoanDto>(loan);
        result.BookTitle = book.Title;
        return result;
    }

    // 3) Al devolver: Stock++
    public async Task<LoanDto> ReturnLoanAsync(int loanId)
    {
        var loan = await _uow.Loans.GetByIdAsync(loanId)
                   ?? throw new InvalidOperationException("Préstamo no encontrado.");

        if (loan.Status == "Returned")
            throw new InvalidOperationException("El préstamo ya fue devuelto.");

        var book = await _uow.Books.GetByIdAsync(loan.BookId)
                   ?? throw new InvalidOperationException("Libro no encontrado.");

        loan.Status = "Returned";
        loan.ReturnDate = DateTime.UtcNow;
        book.Stock += 1;

        await _uow.SaveChangesAsync();

        var result = _mapper.Map<LoanDto>(loan);
        result.BookTitle = book.Title;
        return result;
    }

    public async Task<IEnumerable<LoanDto>> GetActiveLoansAsync()
    {
        var loans = await _uow.Loans.GetActiveLoansAsync();
        return _mapper.Map<IEnumerable<LoanDto>>(loans);
    }

    public async Task<LoanDto?> GetByIdAsync(int id)
    {
        var loan = await _uow.Loans.GetByIdAsync(id);
        if (loan is null) return null;

        // Si en el repo no hiciste Include(Book),
        // puedes buscar el libro aparte
        var book = await _uow.Books.GetByIdAsync(loan.BookId);
        var result = _mapper.Map<LoanDto>(loan);
        if (book != null)
            result.BookTitle = book.Title;
        return result;
    }
}
