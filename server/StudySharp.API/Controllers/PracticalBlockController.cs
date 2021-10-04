using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.PracticalBlocks;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.API.Responses.PracticalBlocks;
using StudySharp.API.Responses.TheoryBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/courses")]

    public class PracticalBlockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PracticalBlockController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("{courseId:int}/practical-blocks")]
        public async Task<OperationResult> Add([FromRoute] int courseId, [FromBody] AddPracticalBlockRequest addPracticalBlockRequest)
        {
            var addPracticalBlockCommand = _mapper.Map<AddPracticalBlockCommand>(addPracticalBlockRequest);
            addPracticalBlockCommand.CourseId = courseId;
            return await _mediator.Send(addPracticalBlockCommand);
        }

        [HttpGet("{courseId:int}/practical-blocks/{id:int}")]
        public async Task<OperationResult<GetPracticalBlockByIdResponse>> GetPracticalBlockById([FromRoute] GetPracticalBlockByIdRequest getPracticalBlockByIdRequest)
        {
            var getPracticalBlockByIdQuery = _mapper.Map<GetTheoryBlockByIdQuery>(getPracticalBlockByIdRequest);
            var operationResult = await _mediator.Send(getPracticalBlockByIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetPracticalBlockByIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetPracticalBlockByIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpGet("{courseId:int}/practical-blocks")]
        public async Task<OperationResult<GetPracticalBlocksByCourseIdResponse>> GetPracticalBlocksByCourseId([FromRoute] GetPracticalBlocksByCourseIdRequest getPracticalBlockByCourseIdRequest)
        {
            var getPracticalBlockByCourseIdQuery = _mapper.Map<GetPracticalBlocksByCourseIdQuery>(getPracticalBlockByCourseIdRequest);
            var operationResult = await _mediator.Send(getPracticalBlockByCourseIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetPracticalBlocksByCourseIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetPracticalBlocksByCourseIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpPut("{courseId:int}/practical-blocks/{id:int}")]
        public async Task<OperationResult<UpdatePracticalBlockResponse>> Update([FromRoute] int id, [FromRoute] int courseId, [FromBody] UpdatePracticalBlockByIdRequest updatePracticalBlockByIdRequest)
        {
            var updatePracticalBlockCommand = _mapper.Map<UpdatePracticalBlockCommand>(updatePracticalBlockByIdRequest);
            updatePracticalBlockCommand.Id = id;
            updatePracticalBlockCommand.CourseId = courseId;
            var operationResult = await _mediator.Send(updatePracticalBlockCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<UpdatePracticalBlockResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<UpdatePracticalBlockResponse>(operationResult);
            return OperationResult.Ok(response);
        }

        [HttpDelete("{courseId:int}/practical-blocks/{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemovePracticalBlockByIdRequest removePracticalBlockByIdRequest)
        {
            var removePracticalBlockByIdCommand = _mapper.Map<RemovePracticalBlockByIdCommand>(removePracticalBlockByIdRequest);
            return await _mediator.Send(removePracticalBlockByIdCommand);
        }
    }
}