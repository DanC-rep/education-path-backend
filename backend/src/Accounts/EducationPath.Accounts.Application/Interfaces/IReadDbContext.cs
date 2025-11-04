using EducationPath.Accounts.Contracts.Dtos;

namespace EducationPath.Accounts.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<UserDto> Users { get; }
    
    IQueryable<RoleDto> Roles { get; }
    
    IQueryable<AdminAccountDto> AdminAccounts { get; }
    
    IQueryable<StudentAccountDto> StudentAccounts { get; }
    
    
}