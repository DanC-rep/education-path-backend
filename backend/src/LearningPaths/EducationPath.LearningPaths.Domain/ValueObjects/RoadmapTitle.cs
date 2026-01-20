using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Domain.ValueObjects;

public class RoadmapTitle : ValueObject
{
    public const int MAX_ROADMAP_TITLE_LENGTH = 100;
    
    public string Value { get; private set; }
    
    private RoadmapTitle() { }
    
    private RoadmapTitle(string value) => Value = value;

    public static Result<RoadmapTitle, Error> Create(string lessonTitle)
    {
        if (string.IsNullOrEmpty(lessonTitle))
            return GeneralErrors.ValueIsRequired("roadmap title");

        if (lessonTitle.Length > MAX_ROADMAP_TITLE_LENGTH)
            return GeneralErrors.Length("roadmap title", MAX_ROADMAP_TITLE_LENGTH);

        return new RoadmapTitle(lessonTitle);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}