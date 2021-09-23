using AutoMapper;
using StudySharp.API.Requests.Auth;
using StudySharp.API.Responses.Auth;
using StudySharp.ApplicationServices.Commands.Auth;
using StudySharp.ApplicationServices.JwtAuthService.ResultModels;

namespace StudySharp.API.MapperProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterNewUserRequest, RegisterNewUserCommand>();
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginResult, LoginResponse>();
        }
    }
}
