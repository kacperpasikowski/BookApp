using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Author, GetAuthorDto>();


            CreateMap<Book, SearchResultDto>()
            .ForMember(dest => dest.TitleOrAuthorName, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Book"))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.BookAvatarUrl))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BookCategories))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors))
            .ForMember(dest => dest.MainCategory, opt => opt.Ignore());

            // Mapowanie dla autorów
            CreateMap<Author, SearchResultDto>()
                .ForMember(dest => dest.TitleOrAuthorName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Author"))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AuthorAvatarUrl))
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.MainCategory, opt => opt.Ignore());

            // Mapowanie dla BookAuthor -> BookAuthorsDto
            CreateMap<BookAuthor, BookAuthorsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Author.Name));

            // Mapowanie dla BookCategory -> CategoryDto
            CreateMap<BookCategory, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}