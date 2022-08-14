using AutoMapper;
using Core.RequestModels;
using Core.ViewModels;
using Model;

namespace COMS
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Amounts, AmountRequestModel>().ReverseMap();
            CreateMap<Amounts, AmountResponse>().ReverseMap();

            CreateMap<Attachments, AttachmentResponse>().ReverseMap();
            CreateMap<Attachments, AttachmentRequestModel>().ReverseMap();
            CreateMap<AttachmentTypes, AttachmentTypeResponse>().ReverseMap();
            
            CreateMap<Deposites, DepositRequestModel>().ReverseMap();
            CreateMap<Deposites, DepositResponse>().ReverseMap();
            CreateMap<Deposites, DepositSearchRequestModel>().ReverseMap();
            
            CreateMap<Members, MemberRequestModel>().ReverseMap();
            CreateMap<Members, MemberResponse>().ReverseMap();
            CreateMap<Members, MemberSearchRequestModel>().ReverseMap();

            CreateMap<Roles, RoleResponse>().ReverseMap();
            CreateMap<Roles, RoleRequestModel>().ReverseMap();

            CreateMap<Users, UserViewModel>().ReverseMap();
            CreateMap<Users, UserRequestModel>().ReverseMap();
            CreateMap<Users, UserResponse>().ReverseMap();
        }
    }
}
