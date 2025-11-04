namespace EducationPath.Accounts.Contracts.Dtos;

public class UserRolesDto
{
    public RoleDto Role { get; init; } = null!;
    public Guid RoleId { get; init; }
    
    public UserDto User {get; init; } = null!;
    public Guid UserId { get; init; }
}