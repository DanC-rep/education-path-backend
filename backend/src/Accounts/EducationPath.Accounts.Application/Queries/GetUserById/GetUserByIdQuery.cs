using EducationPath.Core.Abstractions;

namespace EducationPath.Accounts.Application.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery;