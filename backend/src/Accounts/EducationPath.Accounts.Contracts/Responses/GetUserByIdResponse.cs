using EducationPath.Accounts.Contracts.Dtos;

namespace EducationPath.Accounts.Contracts.Responses;

public class GetUserByIdResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;
    
    public string? Patronymic { get; init; }
    
    public string UserName { get; init; } = null!;
    
    public IEnumerable<RoleDto> Roles { get; init; } = [];
    
    public StudentAccountDto? StudentAccount { get; init; }
    
    public AdminAccountDto? AdminAccount { get; init; }

    public static GetUserByIdResponse Create(UserDto user) =>
        new GetUserByIdResponse()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            UserName = user.UserName,
            Roles = user.Roles,
            StudentAccount = user.StudentAccount,
            AdminAccount = user.AdminAccount
        };
}