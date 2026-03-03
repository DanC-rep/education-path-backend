using CSharpFunctionalExtensions;
using EducationPath.Accounts.Domain.Users;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Application.Interfaces;

public interface IAccountsManager
{
    Task AddStudentAccount(
        StudentAccount studentAccount,
        CancellationToken cancellationToken = default);

    Task AddAdminAccount(
        AdminAccount adminAccount, 
        CancellationToken cancellationToken = default);
    
    Task<Result<User, Error>> GetById(Guid id, CancellationToken cancellationToken = default);
}