using CSharpFunctionalExtensions;
using EducationPath.Accounts.Domain.RefreshTokens;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Application.Interfaces;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken = default);
    
    Task<Result<RefreshSession, Error>> GetByUserId(
        Guid userId, CancellationToken cancellationToken = default);

    void Delete(RefreshSession refreshSession);
}