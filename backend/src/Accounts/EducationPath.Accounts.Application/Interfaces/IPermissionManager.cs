using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Application.Interfaces;

public interface IPermissionManager
{
    Task AddRangeIfNotExists(
        IEnumerable<string> permissionsToAdd,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<string>, Error>> GetPermissionByUserId(
        Guid userId,
        CancellationToken cancellationToken = default);
}