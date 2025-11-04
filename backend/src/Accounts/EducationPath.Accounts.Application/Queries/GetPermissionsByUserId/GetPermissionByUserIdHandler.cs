using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Core.Abstractions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Application.Queries.GetPermissionsByUserId;

public class GetPermissionByUserIdHandler : IQueryHandlerWithResult<IEnumerable<string>, GetPermissionsByUserIdQuery>
{
    private readonly IPermissionManager _permissionManager;

    public GetPermissionByUserIdHandler(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task<Result<IEnumerable<string>, ErrorList>> Handle(
        GetPermissionsByUserIdQuery query, 
        CancellationToken cancellationToken = default)
    {
        var result = await _permissionManager.GetPermissionByUserId(query.UserId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrors();

        return result.Value.ToList();
    }
}