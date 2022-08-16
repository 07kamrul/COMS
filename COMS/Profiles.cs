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
            CreateMap<Amount, AmountRequestModel>().ReverseMap();
            CreateMap<Amount, AmountResponse>().ReverseMap();

            CreateMap<Attachment, AttachmentResponse>().ReverseMap();
            CreateMap<Attachment, AttachmentRequestModel>().ReverseMap();
            CreateMap<AttachmentType, AttachmentTypeResponse>().ReverseMap();
            
            CreateMap<Deposite, DepositRequestModel>().ReverseMap();
            CreateMap<Deposite, DepositResponse>().ReverseMap();
            CreateMap<Deposite, DepositSearchRequestModel>().ReverseMap();
            
            CreateMap<Member, MemberRequestModel>().ReverseMap();
            CreateMap<Member, MemberResponse>().ReverseMap();
            CreateMap<Member, MemberSearchRequestModel>().ReverseMap();

            CreateMap<Role, RoleResponse>().ReverseMap();
            CreateMap<Role, RoleRequestModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserRequestModel>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
