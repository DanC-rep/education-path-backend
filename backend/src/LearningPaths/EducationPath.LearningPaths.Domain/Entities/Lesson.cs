using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.LearningPaths.Domain.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.LearningPaths.Domain.Entities;

public class Lesson : Entity<LessonId>
{
    public LessonTitle Title { get; private set; }
    
    public LessonContent Content { get; private set; }
    
    public bool IsCompleted { get; private set; }
    
    public LessonType Type { get; private set; }
    
    public RoadmapId RoadmapId { get; private set; }

    public IReadOnlyList<Link>? Links => _links;

    private readonly List<Link>? _links;

    private Lesson(LessonId id) : base(id) { }

    public Lesson(
        LessonId id,
        LessonTitle title,
        LessonContent content,
        RoadmapId roadmapId,
        IEnumerable<Link>? additionalLinks = null) : base(id)
    {
        Title = title;
        Content = content;
        RoadmapId = roadmapId;
        _links = additionalLinks?.ToList();
    }
}