using AutoMapper;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Web.Models;

namespace PR.OpenGym.Web.Configurations
{
    public class MyMapperConfiguration : Profile
    {
        public MyMapperConfiguration()
        {
            CreateMap<Associate, AssociateViewModel>().ReverseMap();
            CreateMap<AssociateDetails, AssociateDetailsViewModel>().ReverseMap();
            CreateMap<Membership, MembershipViewModel>().ReverseMap();


            CreateMap<AssociatePostDTO,AssociateViewModel>().ReverseMap();
            CreateMap<AssociateGetDTO,AssociateViewModel>().ReverseMap();
            CreateMap<AssociateMembershipDTO, AssociateMembershipViewModel>().ReverseMap();
        }  
    }
}
