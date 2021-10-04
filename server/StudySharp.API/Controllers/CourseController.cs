using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.Courses;
using StudySharp.API.Responses.Courses;
using StudySharp.ApplicationServices.Commands;
using StudySharp.ApplicationServices.Queries;
using StudySharp.Domain.General;

namespace StudySharp.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/courses")]

    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CourseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<OperationResult> Add([FromBody] AddCourseRequest addCourseRequest)
        {
            var addCourseCommand = _mapper.Map<AddCourseCommand>(addCourseRequest);
            return await _mediator.Send(addCourseCommand);
        }

        [HttpGet("{id:int}")]
        public async Task<OperationResult<GetCourseByIdResponse>> GetCourseById([FromRoute] GetCourseByIdRequest getCourseByIdRequest)
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
        [Route("~/api/teachers/{teacherId:int}/courses")]
        public async Task<OperationResult<GetCoursesByTeacherIdResponse>> GetCoursesByTeacherId([FromRoute] GetCoursesByTeacherIdRequest getCoursesByTeacherIdRequest)
        {
            var getCoursesByTeacherIdQuery = _mapper.Map<GetCoursesByTeacherIdQuery>(getCoursesByTeacherIdRequest);
            var operationResult = await _mediator.Send(getCoursesByTeacherIdQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetCoursesByTeacherIdResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetCoursesByTeacherIdResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpGet]
        public async Task<OperationResult<GetCoursesResponse>> GetCourses([FromBody] GetCoursesRequest getCoursesRequest)
        {
            var getCoursesQuery = _mapper.Map<GetCoursesQuery>(getCoursesRequest);
            var operationResult = await _mediator.Send(getCoursesQuery);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<GetCoursesResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<GetCoursesResponse>(operationResult.Result);
            return OperationResult.Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<OperationResult<UpdateCourseResponse>> Update([FromRoute] int id, [FromBody] UpdateCourseByIdRequest updateCourseByIdRequest)
        {
            var updateCourseCommand = _mapper.Map<UpdateCourseCommand>(updateCourseByIdRequest);
            updateCourseCommand.Id = id;
            var operationResult = await _mediator.Send(updateCourseCommand);

            if (!operationResult.IsSucceeded)
            {
                return OperationResult.Fail<UpdateCourseResponse>(operationResult.Errors);
            }

            var response = _mapper.Map<UpdateCourseResponse>(operationResult);
            return OperationResult.Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemoveCourseByIdRequest removeCourseByIdRequest)
        {
            var removeCourseByIdCommand = _mapper.Map<RemoveCourseByIdCommand>(removeCourseByIdRequest);
            return await _mediator.Send(removeCourseByIdCommand);
        }
    }
}
