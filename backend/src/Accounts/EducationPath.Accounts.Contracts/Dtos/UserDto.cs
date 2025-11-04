namespace EducationPath.Accounts.Contracts.Dtos;

public class UserDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
    
    public string Surname { get; init; } = null!;
    
    public string? Patronymic { get; init; }
    
    public string UserName { get; init; } = null!;

    public List<RoleDto> Roles { get; init; } = [];
    
    public AdminAccountDto? AdminAccount { get; init; }
    
    public StudentAccountDto? StudentAccount { get; init; }
}