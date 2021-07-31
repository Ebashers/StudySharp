﻿using System.Collections.Generic;
using StudySharp.Domain.Models.Learning;

namespace StudySharp.Domain.Models.Users
{
    public sealed class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; }
    }
}