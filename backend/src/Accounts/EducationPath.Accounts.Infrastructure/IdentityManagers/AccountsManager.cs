using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Infrastructure.IdentityManagers;

public class AccountsManager : IAccountsManager
{
    private readonly AccountsWriteDbContext _accountsWriteDbContext;

    public AccountsManager(AccountsWriteDbContext accountsWriteDbContext)
    {
        _accountsWriteDbContext = accountsWriteDbContext;
    }
    
    public async Task AddStudentAccount(
        StudentAccount studentAccount, 
        CancellationToken cancellationToken = default)
    {
        await _accountsWriteDbContext.StudentAccounts.AddAsync(studentAccount, cancellationToken);
    }

    public async Task AddAdminAccount(AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        await _accountsWriteDbContext.AdminAccounts.AddAsync(adminAccount, cancellationToken);
    }

    public async Task<Result<User, Error>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _accountsWriteDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user is null)
            return GeneralErrors.NotFound(id, "user");

        return user;
    }
}