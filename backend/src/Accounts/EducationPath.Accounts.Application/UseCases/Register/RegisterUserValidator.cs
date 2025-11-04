using EducationPath.Accounts.Domain.Users.ValueObjects;
using EducationPath.Core.Validation;
using FluentValidation;

namespace EducationPath.Accounts.Application.UseCases.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.FullName)
            .MustBeValueObject(f =>
                FullName.Create(f.Name, f.Surname, f.Patronymic));
    }
}