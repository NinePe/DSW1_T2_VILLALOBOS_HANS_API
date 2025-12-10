namespace Library.Domain.Entities;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = null!; // "Active", "Returned"
    public DateTime CreatedAt { get; set; }
}