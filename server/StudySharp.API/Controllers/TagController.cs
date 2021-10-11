using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.Tags;
using StudySharp.API.Responses.Tags;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/courses")]

    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TagController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<OperationResult> Add([FromBody] AddTagRequest addTagRequest)
        {
            var addTagCommand = _mapper.Map<AddTagCommand>(addTagRequest);
            return await _mediator.Send(addTagCommand);
        }

        [HttpGet("{id:int}")]
        public async Task<OperationResult<GetTagByIdResponse>> GetTagById([FromRoute] GetTagByIdRequest getTagByIdRequest)
        {
            var getTagByIdQuery = _mapper.Map<GetTagByIdQuery>(getTagByIdRequest);
            var operationResult = await _mediator.Send(getTagByIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetTagByIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetTagByIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<OperationResult> Remove([FromBody] RemoveTagByIdRequest removeTagByIdRequest)
        {
            var removeTagByIdCommand = _mapper.Map<RemoveTagByIdCommand>(removeTagByIdRequest);
            return await _mediator.Send(removeTagByIdCommand);
        }
    }
}