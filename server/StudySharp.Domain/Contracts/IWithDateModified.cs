using System;

namespace StudySharp.Domain.Contracts
{
    public interface IWithDateModified
    {
        DateTimeOffset? DateModified { get; }

        void SetDateModified(DateTimeOffset value);
    }
}
