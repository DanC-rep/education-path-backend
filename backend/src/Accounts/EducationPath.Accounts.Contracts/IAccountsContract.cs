using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Contracts;

public interface IAccountsContract
{
    Task<UnitResult<Error>> CheckUserExists(Guid userId, CancellationToken cancellationToken = default);
}