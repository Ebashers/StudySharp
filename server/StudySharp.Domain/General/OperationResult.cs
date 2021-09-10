using System.Collections.Generic;

namespace StudySharp.Domain.General
{
    public sealed class OperationResult<T> where T : class
    {
        public T Result { get; set; }
        public bool IsSucceeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
