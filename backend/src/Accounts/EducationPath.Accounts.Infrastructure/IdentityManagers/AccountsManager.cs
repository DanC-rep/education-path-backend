using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Infrastructure.DbContexts;

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
}