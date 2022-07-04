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

            CreateMap<Attachment, AttachmentResponse>().ReverseMap();
            CreateMap<Attachment, AttachmentRequestModel>().ReverseMap();
            CreateMap<AttachmentType, AttachmentTypeResponse>().ReverseMap();
            
            CreateMap<Deposites, DepositRequestModel>().ReverseMap();
            CreateMap<Deposites, DepositResponse>().ReverseMap();
            CreateMap<Deposites, DepositSearchRequestModel>().ReverseMap();
            
            CreateMap<Members, MemberRequestModel>().ReverseMap();
            CreateMap<Members, MemberResponse>().ReverseMap();
            CreateMap<Members, MemberSearchRequestModel>().ReverseMap();

            CreateMap<Role, RoleResponse>().ReverseMap();
            CreateMap<Role, RoleReqquestModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserRequestModel>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
