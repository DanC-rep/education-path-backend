using EducationPath.Core.Validation;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.Skills.Domain.ValueObjects;
using FluentValidation;

namespace EducationPath.Skills.Application.UseCases.UpdateSkill;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillValidator()
    {
        RuleFor(s => s.Name)
            .MustBeValueObject(Name.Create);
        
        RuleFor(s => s.Description)
            .MustBeValueObject(Description.Create);
    }
}