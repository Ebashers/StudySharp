using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.Courses;
using StudySharp.API.Responses.Courses;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CourseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<OperationResult> Add([FromBody] AddCourseRequest addCourseRequest)
        {
            var addCourseCommand = _mapper.Map<AddCourseCommand>(addCourseRequest);
            return await _mediator.Send(addCourseCommand);
        }

        [HttpPost("remove")]
        public async Task<OperationResult> Remove([FromBody] RemoveCourseByIdRequest removeCourseByIdRequest)
        {
            var removeCourseByIdCommand = _mapper.Map<RemoveCourseByIdCommand>(removeCourseByIdRequest);
            return await _mediator.Send(removeCourseByIdCommand);
        }

        [HttpGet]
        public async Task<OperationResult> GetCourseById([FromBody] GetCourseByIdRequest getCourseByIdRequest)
        {
            var getCourseByIdQuery = _mapper.Map<GetCourseByIdQuery>(getCourseByIdRequest);
            var operationResult = await _mediator.Send(getCourseByIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetCourseByIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetCourseByIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpGet]
        public async Task<OperationResult> GetCourses([FromBody] GetCoursesRequest getCoursesRequest)
        {
            var getCoursesQuery = _mapper.Map<GetCourseByIdQuery>(getCoursesRequest);
            var operationResult = await _mediator.Send(getCoursesQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetCoursesResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetCoursesResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }
    }
}
