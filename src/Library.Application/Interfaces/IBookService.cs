using Library.Application.DTOs.Book;

namespace Library.Application.Interfaces;

public interface IBookService
{
    Task<BookDto> CreateAsync(CreateBookDto dto);
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> UpdateAsync(int id, CreateBookDto dto);
Task DeleteAsync(int id);

}