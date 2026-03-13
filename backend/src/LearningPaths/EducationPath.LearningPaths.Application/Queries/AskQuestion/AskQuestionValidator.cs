using EducationPath.Core.Validation;
using EducationPath.SharedKernel.Errors;
using FluentValidation;

namespace EducationPath.LearningPaths.Application.Queries.AskQuestion;

public class AskQuestionValidator : AbstractValidator<AskQuestionQuery>
{
    public AskQuestionValidator()
    {
        RuleFor(c => c.Question)
            .NotEmpty()
            .WithError(GeneralErrors.ValueIsInvalid("question"));
    }

}
