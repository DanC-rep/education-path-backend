using EducationPath.Accounts.Contracts.Requests;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Dtos;

namespace EducationPath.Accounts.Application.UseCases.Register;

public record RegisterUserCommand(
    string Email,
    string UserName,
    FullNameDto FullName,
    string Password) : ICommand
{
    public static RegisterUserCommand Create(RegisterUserRequest request) =>
        new(request.Email,
            request.UserName,
            request.FullName,
            request.Password);
}