using EducationPath.Accounts.Domain.Users;

namespace EducationPath.Accounts.Application.Interfaces;

public interface IAccountsManager
{
    Task AddStudentAccount(
        StudentAccount studentAccount,
        CancellationToken cancellationToken = default);

    Task AddAdminAccount(
        AdminAccount adminAccount, 
        CancellationToken cancellationToken = default);
}