using AutoMapper;
using Library.Application.DTOs.Book;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public BookService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var book = _mapper.Map<Book>(dto);
        book.CreatedAt = DateTime.UtcNow;

        await _uow.Books.AddAsync(book);
        await _uow.SaveChangesAsync();

        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _uow.Books.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _uow.Books.GetByIdAsync(id);
        return book is null ? null : _mapper.Map<BookDto>(book);
    }
    
    public async Task<BookDto> UpdateAsync(int id, CreateBookDto dto)
{
    var book = await _uow.Books.GetByIdAsync(id)
               ?? throw new InvalidOperationException("Libro no encontrado.");

    book.Title = dto.Title;
    book.Author = dto.Author;
    book.ISBN = dto.ISBN;
    book.Stock = dto.Stock;

    _uow.Books.Update(book);
    await _uow.SaveChangesAsync();

    return _mapper.Map<BookDto>(book);
}

public async Task DeleteAsync(int id)
{
    var book = await _uow.Books.GetByIdAsync(id)
               ?? throw new InvalidOperationException("Libro no encontrado.");

    _uow.Books.Remove(book);
    await _uow.SaveChangesAsync();
}

}
