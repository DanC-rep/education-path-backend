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

    public IReadOnlyList<LessonDependency>? OutgoingDependencies;
    private readonly List<LessonDependency> _outgoingDependencies = [];

    public IReadOnlyList<LessonDependency>? IncomingDependencies;
    private readonly List<LessonDependency> _incomingDependencies = [];

    private Lesson(LessonId id) : base(id) { }

    public Lesson(
        LessonId id,
        LessonTitle title,
        LessonContent content,
        RoadmapId roadmapId,
        LessonType type,
        IEnumerable<Link>? additionalLinks = null) : base(id)
    {
        Title = title;
        Content = content;
        RoadmapId = roadmapId;
        Type = type;
        _links = additionalLinks?.ToList();
    }
}