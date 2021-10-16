using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudySharp.API.Requests.Courses;
using StudySharp.API.Requests.PracticalBlocks;
using StudySharp.API.Requests.TheoryBlocks;
using StudySharp.API.Responses.Courses;
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

        // did not work because of mapper exception (wtf)
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

        [HttpPost("{courseId:int}/theory-blocks")]
        public async Task<OperationResult> Add([FromRoute] int courseId, [FromBody] AddTheoryBlockRequest addTheoryBlockRequest)
        {
            var addTheoryBlockCommand = _mapper.Map<AddTheoryBlockCommand>(addTheoryBlockRequest);
            addTheoryBlockCommand.CourseId = courseId;
            return await _mediator.Send(addTheoryBlockCommand);
        }

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

        [HttpDelete("{courseId:int}/theory-blocks/{id:int}")]
        public async Task<OperationResult> Remove([FromRoute] RemoveTheoryBlockByIdRequest removeTheoryBlockByIdRequest)
        {
            var removeTheoryBlockByIdCommand = _mapper.Map<RemoveTheoryBlockByIdCommand>(removeTheoryBlockByIdRequest);
            return await _mediator.Send(removeTheoryBlockByIdCommand);
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
            var getPracticalBlockByIdQuery = _mapper.Map<GetPracticalBlockByIdQuery>(getPracticalBlockByIdRequest);
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
