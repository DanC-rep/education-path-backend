using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Contracts;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly IReadDbContext _readDbContext;

    public AccountsContract(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<UnitResult<Error>> CheckUserExists(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _readDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
            return GeneralErrors.NotFound(userId, "user");

        return Result.Success<Error>();
    }
}