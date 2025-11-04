using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel;

public abstract class DomainEntity<TId> : Entity<TId>
    where TId : IComparable<TId>
{
    protected DomainEntity(TId id)
        : base(id)
    {
    }
}