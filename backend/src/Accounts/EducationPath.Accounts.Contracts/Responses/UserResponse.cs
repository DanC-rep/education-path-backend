namespace EducationPath.Accounts.Contracts.Responses;

public record UserResponse(Guid Id, string Email, string UserName, IEnumerable<string> Roles);