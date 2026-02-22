using EducationPath.LearningPaths.Domain.Enums;

namespace EducationPath.LearningPaths.Application.Converters;

public static class LessonTypeConverter
{
    public static string Convert(LessonType type)
    {
        return type switch
        {
            LessonType.Required => "Required",
            LessonType.Optional => "Optional",
            LessonType.Recommended => "Recommended",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}