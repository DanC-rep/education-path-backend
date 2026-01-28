using EducationPath.LearningPaths.Domain.Enums;

namespace EducationPath.LearningPaths.Application.Converters;

public static class RoadmapLevelConverter
{
    public static string Convert(RoadmapLevel level)
    {
        return level switch
        {
            RoadmapLevel.Beginning => "Beginning",
            RoadmapLevel.Basic => "Basic",
            RoadmapLevel.Advanced => "Advanced",
            _ => "Basic"
        };
    }
}