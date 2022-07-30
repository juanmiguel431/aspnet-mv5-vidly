using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<MembershipType, MembershipTypeDto>();
            
            CreateMap<MovieDto, Movie>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}