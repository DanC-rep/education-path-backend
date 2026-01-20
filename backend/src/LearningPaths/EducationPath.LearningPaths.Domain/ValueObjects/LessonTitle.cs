using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Domain.ValueObjects;

public class LessonTitle : ValueObject
{
    public const int MAX_LESSON_TITLE_LENGTH = 100;
    
    public string Value { get; private set; }
    
    private LessonTitle() { }
    
    private LessonTitle(string value) => Value = value;

    public static Result<LessonTitle, Error> Create(string lessonTitle)
    {
        if (string.IsNullOrEmpty(lessonTitle))
            return GeneralErrors.ValueIsRequired("lesson title");

        if (lessonTitle.Length > MAX_LESSON_TITLE_LENGTH)
            return GeneralErrors.Length("lesson title", MAX_LESSON_TITLE_LENGTH);

        return new LessonTitle(lessonTitle);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}