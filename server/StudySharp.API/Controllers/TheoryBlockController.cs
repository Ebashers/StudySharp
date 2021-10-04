using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.API.Responses.TheoryBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/courses")]

    public class TheoryBlockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TheoryBlockController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // +2 works
        [HttpPost("{courseId:int}/theory-blocks")]
        public async Task<OperationResult> Add([FromRoute] int courseId, [FromBody] AddTheoryBlockRequest addTheoryBlockRequest)
        {
            var addTheoryBlockCommand = _mapper.Map<AddTheoryBlockCommand>(addTheoryBlockRequest);
            addTheoryBlockCommand.CourseId = courseId;
            return await _mediator.Send(addTheoryBlockCommand);
        }

        // +2 works
        [HttpDelete("{courseId:int}/theory-blocks/{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemoveTheoryBlockByIdRequest removeTheoryBlockByIdRequest)
        {
            var removeTheoryBlockByIdCommand = _mapper.Map<RemoveTheoryBlockByIdCommand>(removeTheoryBlockByIdRequest);
            return await _mediator.Send(removeTheoryBlockByIdCommand);
        }

        // +2 works
        [HttpGet("{courseId:int}/theory-blocks/{id:int}")]
        public async Task<OperationResult<GetTheoryBlockByIdResponse>> GetTheoryBlockById([FromRoute] GetTheoryBlockByIdRequest getTheoryBlockByIdRequest)
        {
            var getTheoryBlockByIdQuery = _mapper.Map<GetTheoryBlockByIdQuery>(getTheoryBlockByIdRequest);
            var operationResult = await _mediator.Send(getTheoryBlockByIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetTheoryBlockByIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetTheoryBlockByIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        // +2 works
        [HttpPut("{courseId:int}/theory-blocks/{id:int}")]
        public async Task<OperationResult<UpdateTheoryBlockResponse>> Update([FromRoute] int id, [FromRoute] int courseId, [FromBody] UpdateTheoryBlockByIdRequest updateTheoryBlockByIdRequest)
        {
            var updateTheoryBlockCommand = _mapper.Map<UpdateTheoryBlockCommand>(updateTheoryBlockByIdRequest);
            updateTheoryBlockCommand.Id = id;
            updateTheoryBlockCommand.CourseId = courseId;
            var operationResult = await _mediator.Send(updateTheoryBlockCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<UpdateTheoryBlockResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<UpdateTheoryBlockResponse>(operationResult);
            return OperationResult.Ok(response);
        }

        // +2 works
        [HttpGet("{courseId:int}/theory-blocks")]
        public async Task<OperationResult<GetTheoryBlocksByCourseIdResponse>> GetTheoryBlocksByCourseId([FromRoute] GetTheoryBlocksByCourseIdRequest getTheoryBlockByCourseIdRequest)
        {
            var getTheoryBlockByCourseIdQuery = _mapper.Map<GetTheoryBlocksByCourseIdQuery>(getTheoryBlockByCourseIdRequest);
            var operationResult = await _mediator.Send(getTheoryBlockByCourseIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetTheoryBlocksByCourseIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetTheoryBlocksByCourseIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }
    }
}