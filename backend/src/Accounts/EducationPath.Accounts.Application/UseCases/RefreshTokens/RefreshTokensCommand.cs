using EducationPath.Core.Abstractions;

namespace EducationPath.Accounts.Application.UseCases.RefreshTokens;

public record RefreshTokensCommand(Guid RefreshToken) : ICommand;