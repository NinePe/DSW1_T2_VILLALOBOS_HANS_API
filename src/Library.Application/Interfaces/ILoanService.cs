using Library.Application.DTOs.Loan;

namespace Library.Application.Interfaces;

public interface ILoanService
{
    Task<LoanDto> CreateLoanAsync(CreateLoanDto dto);    // regla: no prestar si stock = 0
    Task<LoanDto> ReturnLoanAsync(int loanId);           // regla: aumentar stock

    Task<IEnumerable<LoanDto>> GetActiveLoansAsync();
Task<LoanDto?> GetByIdAsync(int id); // si quieres usar el GET /{id}

}