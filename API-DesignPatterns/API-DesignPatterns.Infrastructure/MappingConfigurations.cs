using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.DTOs;
using AutoMapper;

namespace API_DesignPatterns.Infrastructure
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AddAuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<AddBookDto, Book>();
        }
    }
}
