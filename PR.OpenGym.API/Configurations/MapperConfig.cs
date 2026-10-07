using AutoMapper;
using PR.OpenGym.API.ViewModel;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AssociatePostDTO,Associate>().ReverseMap();
            CreateMap<AssociateGetDTO,Associate>().ReverseMap();
            CreateMap<AssociateMembershipDTO,AssociateMembership>().ReverseMap();

        }
    }
}
