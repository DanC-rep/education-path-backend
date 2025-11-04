using EducationPath.Core.Abstractions;

namespace EducationPath.Accounts.Application.UseCases.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;