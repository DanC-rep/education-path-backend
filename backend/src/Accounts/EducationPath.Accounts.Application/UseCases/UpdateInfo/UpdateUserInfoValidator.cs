using EducationPath.Accounts.Domain.Users.ValueObjects;
using EducationPath.Core.Validation;
using FluentValidation;

namespace EducationPath.Accounts.Application.UseCases.UpdateInfo;

public class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(c => c.FullName)
            .MustBeValueObject(f =>
                FullName.Create(f.Name, f.Surname, f.Patronymic));
    }
}