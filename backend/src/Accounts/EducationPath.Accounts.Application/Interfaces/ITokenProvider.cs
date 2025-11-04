using EducationPath.Accounts.Application.Models;
using EducationPath.Accounts.Domain.Users;

namespace EducationPath.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(
        User user,
        CancellationToken cancellationToken = default);

    Task<Guid> GenerateRefreshToken(User user,
        Guid accessTokenJti,
        CancellationToken cancellationToken = default);
}