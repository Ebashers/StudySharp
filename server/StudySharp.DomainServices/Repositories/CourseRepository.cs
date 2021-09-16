using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudySharp.Domain.General;
using StudySharp.Domain.Models;
using StudySharp.Domain.Repositories;

namespace StudySharp.DomainServices.Repositories
{
    public sealed class CourseRepository : ICourseRepository
    {
        private readonly StudySharpDbContext _context;

        public CourseRepository(StudySharpDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<Course>> CreateCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return new OperationResult<Course> { Result = course, IsSucceeded = true };
        }

        public async Task<OperationResult<Course>> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return BuildErrorResponse("Could not find Course`s Id");
            }

            return new OperationResult<Course> { Result = course, IsSucceeded = true };
        }

        public async Task<OperationResult<List<Course>>> GetCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return new OperationResult<List<Course>> { Result = courses, IsSucceeded = true };
        }

        public async Task<OperationResult<List<Course>>> GetCoursesByUserIdAsync(int userId)
        {
            var courses = await _context.Courses.Where(_ => _.Teacher.UserId == userId).ToListAsync();
            return new OperationResult<List<Course>> { Result = courses, IsSucceeded = true };
        }

        public async Task<OperationResult<Course>> UpdateCourseAsync(Course course)
        {
            if (course == null)
            {
                return BuildErrorResponse("There`s no TheoryBlock you can modify");
            }

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new OperationResult<Course> { Result = course, IsSucceeded = true };
        }

        public async Task<OperationResult<Course>> RemoveCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return BuildErrorResponse("Could not find TheoryBlock`s Id");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return new OperationResult<Course> { Result = course, IsSucceeded = true };
        }

        private OperationResult<Course> BuildErrorResponse(string errorText)
        {
            var result = new OperationResult<Course> { Result = null, IsSucceeded = false };
            result.Errors.Add(errorText);
            return result;
        }
    }
}