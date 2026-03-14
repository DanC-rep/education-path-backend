using EducationPath.Core.Abstractions;

namespace EducationPath.Accounts.Application.UseCases.Logout;

public record LogoutCommand(Guid UserId) : ICommand;