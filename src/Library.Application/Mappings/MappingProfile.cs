using AutoMapper;
using Library.Application.DTOs.Book;
using Library.Application.DTOs.Loan;
using Library.Domain.Entities;

namespace Library.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<Book, BookDto>();

        CreateMap<CreateLoanDto, Loan>();
        CreateMap<Loan, LoanDto>()
            .ForMember(dest => dest.BookTitle,
                       opt => opt.MapFrom(src => src.Book.Title));
    }
}
