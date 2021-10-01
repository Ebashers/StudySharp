using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.PracticalBlocks;
using StudySharp.API.Responses.PracticalBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class PracticalBlockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PracticalBlockController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<OperationResult> Add([FromBody] AddPracticalBlockRequest addPracticalBlockRequest)
        {
            var addCourseCommand = _mapper.Map<AddCourseCommand>(addPracticalBlockRequest);
            return await _mediator.Send(addCourseCommand);
        }

        [HttpDelete("{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemovePracticalBlockByIdRequest removePracticalBlockByIdRequest)
        {
            var removePracticalBlockByIdCommand = _mapper.Map<RemovePracticalBlockByIdCommand>(removePracticalBlockByIdRequest);
            return await _mediator.Send(removePracticalBlockByIdCommand);
        }

        [HttpGet("{id:int}")]
        public async Task<OperationResult> GetPracticalBlockById([FromRoute] GetPracticalBlockByIdRequest getPracticalBlockByIdRequest)
        {
            var getPracticalBlockByIdQuery = _mapper.Map<GetPracticalBlockByIdQuery>(getPracticalBlockByIdRequest);
            var operationResult = await _mediator.Send(getPracticalBlockByIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetPracticalBlockByIdRequest>(operationResult.Errors);
            }

            var response = _mapper.Map<GetPracticalBlockByIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<OperationResult> UpdatePracticalBlock([FromRoute] UpdatePracticalBlockRequest updatePracticalBlockRequest)
        {
            var updatePracticalBlockCommand = _mapper.Map<UpdatePracticalBlockCommand>(updatePracticalBlockRequest);
            return await _mediator.Send(updatePracticalBlockCommand);
        }
    }
}
