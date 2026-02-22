using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Domain.ValueObjects;

public class LessonContent : ValueObject
{
    public const int MAX_LESSON_CONTENT_LENGTH = 5000;
    
    public string Value { get; private set; }
    
    private LessonContent() { }
    
    private LessonContent(string value) => Value = value;

    public static Result<LessonContent, Error> Create(string lessonContent)
    {
        if (string.IsNullOrEmpty(lessonContent))
            return GeneralErrors.ValueIsRequired("lesson content");

        if (lessonContent.Length > MAX_LESSON_CONTENT_LENGTH)
            return GeneralErrors.Length("lesson content", MAX_LESSON_CONTENT_LENGTH);

        return new LessonContent(lessonContent);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}