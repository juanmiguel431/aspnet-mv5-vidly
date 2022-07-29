using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap<MovieDto, Movie>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<Movie, MovieDto>();
        }
    }
}