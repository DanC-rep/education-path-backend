using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.LearningPaths.Domain.ValueObjects;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.LearningPaths.Domain.Entities;

public class Roadmap : Entity<RoadmapId>
{
    public RoadmapTitle Title { get; private set; }
    
    public Description Description { get; private set; }
    
    public RoadmapLevel Level { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public IReadOnlyList<Lesson> Lessons => _lessons;

    private readonly List<Lesson> _lessons = [];

    public IReadOnlyList<LessonDependency> LessonsDependencies => _lessonsDependencies;

    private readonly List<LessonDependency> _lessonsDependencies = [];

    private Roadmap(RoadmapId id) : base(id) { }

    public Roadmap(
        RoadmapId id,
        RoadmapTitle title,
        Description description,
        RoadmapLevel level,
        Guid userId) : base(id)
    {
        Title = title;
        Description = description;
        Level = level;
        UserId = userId;
    }
}