namespace EducationPath.Accounts.Application.Interfaces;

public interface IRolePermissionManager
{
    Task AddRangeIfNotExists(
        Guid roleId,
        IEnumerable<string> permissions,
        CancellationToken cancellationToken = default);
}