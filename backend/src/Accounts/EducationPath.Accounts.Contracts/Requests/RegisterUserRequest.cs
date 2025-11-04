using EducationPath.Core.Dtos;

namespace EducationPath.Accounts.Contracts.Requests;

public record RegisterUserRequest(
    string Email,
    string UserName,
    FullNameDto FullName,
    string Password);