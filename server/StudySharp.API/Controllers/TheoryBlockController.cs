using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.API.Responses.TheoryBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/theory-blocks")]

    public class TheoryBlockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TheoryBlockController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<OperationResult> Add([FromBody] AddTheoryBlockRequest addTheoryBlockRequest)
        {
            var addTheoryBlockCommand = _mapper.Map<AddTheoryBlockCommand>(addTheoryBlockRequest);
            return await _mediator.Send(addTheoryBlockCommand);
        }

        [HttpDelete("{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemoveTheoryBlockByIdRequest removeTheoryBlockByIdRequest)
        {
            var removeTheoryBlockByIdCommand = _mapper.Map<RemoveTheoryBlockByIdCommand>(removeTheoryBlockByIdRequest);
            return await _mediator.Send(removeTheoryBlockByIdCommand);
        }

        [HttpGet("{id:int}")]
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

        [HttpPut("{id:int}")]
        public async Task<OperationResult<UpdateTheoryBlockResponce>> Update([FromRoute] UpdateTheoryBlockByIdRequest updateTheoryBlockByIdRequest)
        {
            var updateTheoryBlockCommand = _mapper.Map<UpdateTheoryBlockCommand>(updateTheoryBlockByIdRequest);
            var operationResult = await _mediator.Send(updateTheoryBlockCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<UpdateTheoryBlockResponce>(operationResult.Errors);
            }

            var response = _mapper.Map<UpdateTheoryBlockResponce>(operationResult);
            return OperationResult.Ok(response);
        }

        [HttpGet("{courseId:int}")]
        public async Task<OperationResult<GetTheoryBlockByCourseIdResponse>> GetTheoryBlockByCourseId([FromRoute] GetTheoryBlockByCourseIdRequest getTheoryBlockByCourseIdRequest)
        {
            var getTheoryBlockByCourseIdQuery = _mapper.Map<GetTheoryBlockByCourseIdQuery>(getTheoryBlockByCourseIdRequest);
            var operationResult = await _mediator.Send(getTheoryBlockByCourseIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetTheoryBlockByCourseIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetTheoryBlockByCourseIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }
    }
}