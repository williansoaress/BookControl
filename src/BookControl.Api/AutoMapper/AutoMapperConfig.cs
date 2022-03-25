using AutoMapper;
using BookControl.Api.DTOs;
using BookControl.Business.Models;

namespace BookControl.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
