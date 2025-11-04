using EducationPath.Core.Abstractions;

namespace EducationPath.Accounts.Application.Queries.GetPermissionsByUserId;

public record GetPermissionsByUserIdQuery(Guid UserId) : IQuery;