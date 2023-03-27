using AutoMapper;
using Lean.Contracts.Administration;
using Lean.Contracts.Blog.Dto;
using Lean.Domain.DBEntities.Administration;
using Lean.Domain.DBEntities.Blog;

namespace Lean.Persistence.Mapper
{
    public class ApplicationMapperProfiler:Profile
    {
        public ApplicationMapperProfiler()
        {

            CreateMap<User, UserRegistrationInputDto>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(srcO => srcO.FirstName))
                .ReverseMap();

            CreateMap<Post, PostInputDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(srcO => srcO.Id))
                .ReverseMap();
            CreateMap<Post, PostOutputDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(srcO => srcO.Id))
                .ReverseMap();
            CreateMap<Comment, CommentInputDto>()
                .ForMember(dest => dest.Text, src => src.MapFrom(srcO => srcO.Text))
                .ReverseMap();
            


        }
    }
}
