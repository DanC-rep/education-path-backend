using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Application.Models;
using EducationPath.Accounts.Domain.RefreshTokens;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.Framework.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EducationPath.Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly AuthOptions _authOptions;
    private readonly RefreshSessionOptions _refreshSessionOptions;
    private readonly AccountsWriteDbContext _accountsWriteDbContext;
    private readonly IPermissionManager _permissionManager;

    public JwtTokenProvider(
        IOptions<AuthOptions> authOptions,
        IOptions<RefreshSessionOptions> refreshSessionOptions,
        AccountsWriteDbContext accountsWriteDbContext,
        IPermissionManager permissionManager)
    {
        _authOptions = authOptions.Value;
        _refreshSessionOptions = refreshSessionOptions.Value;
        _accountsWriteDbContext = accountsWriteDbContext;
        _permissionManager = permissionManager;
    }

    public async Task<JwtTokenResult> GenerateAccessToken(
        User user, 
        CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));

        var permissions = await _permissionManager.GetPermissionByUserId(user.Id, cancellationToken);
        var permissionClaims = permissions.Value.Select(p => new Claim(CustomClaims.Permission, p));

        var jti = Guid.NewGuid();

        Claim[] claims =
        [
            new(CustomClaims.Id, user.Id.ToString()),
            new(CustomClaims.Email, user.Email ?? string.Empty),
            new(CustomClaims.Jti, jti.ToString())
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();
        
        var jwtToken = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_authOptions.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims);

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokenResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user,
        Guid accessTokenJti,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            CreationDate = DateTime.UtcNow,
            ExpirationToken = DateTime.UtcNow.AddDays(int.Parse(_refreshSessionOptions.ExpiredDaysTime)),
            Jti = accessTokenJti,
            RefreshToken = Guid.NewGuid()
        };

        _accountsWriteDbContext.RefreshSessions.Add(refreshSession);
        await _accountsWriteDbContext.SaveChangesAsync(cancellationToken);
        
        return refreshSession.RefreshToken;
    }
}