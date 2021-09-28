using AutoMapper;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;

namespace StudySharp.API.MapperProfiles
{
    public class TheoryBlockProfile : Profile
    {
        public TheoryBlockProfile()
        {
            CreateMap<AddTheoryBlockRequest, AddTheoryBlockCommand>();
            CreateMap<RemoveTheoryBlockByIdRequest, RemoveTheoryBlockByIdCommand>();
            CreateMap<GetTheoryBlockByIdRequest, GetTheoryBlockByIdQuery>();
            CreateMap<UpdateTheoryBlockByIdRequest, UpdateTheoryBlockCommand>();
        }
    }
}