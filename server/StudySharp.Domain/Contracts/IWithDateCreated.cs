using System;

namespace StudySharp.Domain.Contracts
{
    public interface IWithDateCreated
    {
        DateTimeOffset DateCreated { get; }

        void SetDateCreated(DateTimeOffset value);
    }
}
