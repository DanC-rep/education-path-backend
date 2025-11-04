using CSharpFunctionalExtensions;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Domain.Users.ValueObjects;
using EducationPath.SharedKernel.Errors;
using Microsoft.AspNetCore.Identity;

namespace EducationPath.Accounts.Domain.Users;

public class User : IdentityUser<Guid>
{
    public FullName FullName { get; set; }

    public List<Role> Roles { get; private init; } = [];

    public static Result<User, Error> CreateStudent(
        string userName,
        string email,
        FullName fullName,
        Role role)
    {
        if (role.Name != Constants.STUDENT)
            return AuthErrors.InvalidRole();

        return new User
        {
            UserName = userName,
            Email = email,
            FullName = fullName,
            Roles = [role]
        };
    }

    public static Result<User, Error> CreateAdmin(
        string userName,
        string email,
        FullName fullName,
        Role role)
    {
        if (role.Name != Constants.ADMIN)
            return AuthErrors.InvalidRole();

        return new User
        {
            UserName = userName,
            Email = email,
            FullName = fullName,
            Roles = [role]
        };
    }
}