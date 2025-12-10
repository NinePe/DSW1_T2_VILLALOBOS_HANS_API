
namespace Library.Application.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public int Stock { get; set; }
}