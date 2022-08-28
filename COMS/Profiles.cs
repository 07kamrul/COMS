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
            CreateMap<Attachment, AttachmentResponse>().ReverseMap();
            CreateMap<Attachment, AttachmentRequest>().ReverseMap();
            CreateMap<AttachmentType, AttachmentTypeResponse>().ReverseMap();
            
            CreateMap<Member, MemberRequest>().ReverseMap();
            CreateMap<Member, MemberResponse>().ReverseMap();
            CreateMap<Member, MemberSearchRequest>().ReverseMap();

            CreateMap<Account, AccountRequest>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();

            CreateMap<Project, ProjectRequest>().ReverseMap();
            CreateMap<Project, ProjectResponse>().ReverseMap();

            CreateMap<Transaction, TransactionRequest>().ReverseMap();
            CreateMap<Transaction, TransactionResponse>().ReverseMap();

            CreateMap<Role, RoleResponse>().ReverseMap();
            CreateMap<Role, RoleRequest>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
