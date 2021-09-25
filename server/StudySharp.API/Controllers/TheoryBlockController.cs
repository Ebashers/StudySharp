using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.ApplicationServices.Commands;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class TheoryBlockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TheoryBlockController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<OperationResult> Add([FromBody] AddTheoryBlockRequest addTheoryBlockRequest)
        {
            var addTheoryBlockCommand = _mapper.Map<AddTheoryBlockCommand>(addTheoryBlockRequest);
            return await _mediator.Send(addTheoryBlockCommand);
        }

        [HttpPost("remove")]
        public async Task<OperationResult> Remove([FromBody] RemoveTheoryBlockByIdRequest removeTheoryBlockByIdRequest)
        {
            var removeTheoryBlockByIdCommand = _mapper.Map<RemoveTheoryBlockByIdCommand>(removeTheoryBlockByIdRequest);
            return await _mediator.Send(removeTheoryBlockByIdCommand);
        }
    }
}