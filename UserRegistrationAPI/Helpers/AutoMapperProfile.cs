using AutoMapper;
using UserRegistrationAPI.Entities;
using UserRegistrationAPI.Models.Documents;
using UserRegistrationAPI.Models.Users;

namespace UserRegistrationAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserModel>();
            CreateMap<NewUserModel, Users>();
            CreateMap<UpdateModel, Users>();
            CreateMap<DocumentModel, Documents>();
            CreateMap<Documents, DocumentModel>();
        }
    }
}