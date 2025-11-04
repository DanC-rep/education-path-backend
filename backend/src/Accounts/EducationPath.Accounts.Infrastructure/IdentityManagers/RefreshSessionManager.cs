using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.RefreshTokens;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Infrastructure.IdentityManagers;

public class RefreshSessionManager : IRefreshSessionManager
{
    private readonly AccountsWriteDbContext _accountsWriteDbContext;

    public RefreshSessionManager(AccountsWriteDbContext accountsWriteDbContext)
    {
        _accountsWriteDbContext = accountsWriteDbContext;
    }
    
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken = default)
    {
        var refreshSession = await _accountsWriteDbContext.RefreshSessions
            .Include(r => r.User)
            .ThenInclude(u => u.Roles)
            .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, cancellationToken);
        
        if (refreshSession is null)
            return GeneralErrors.NotFound(refreshToken);

        return refreshSession;
    }

    public void Delete(RefreshSession refreshSession)
    {
        _accountsWriteDbContext.RefreshSessions.Remove(refreshSession);
    }
}