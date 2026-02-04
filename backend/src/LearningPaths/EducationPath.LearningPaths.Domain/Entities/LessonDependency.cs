using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.LearningPaths.Domain.Entities;

public class LessonDependency : Entity<LessonDependencyId>
{
    public LessonId FromLessonId { get; private set; }
    public Lesson FromLesson { get; private set; }
    
    public LessonId ToLessonId { get; private set; }
    public Lesson ToLesson { get; private set; }
    
    public RoadmapId RoadmapId { get; private set; }

    private LessonDependency(LessonDependencyId id) : base(id)
    {
    }

    public LessonDependency(
        LessonDependencyId id,
        LessonId fromLesson, 
        LessonId toLesson, 
        RoadmapId roadmapId) : base(id)
    {
        FromLessonId = fromLesson;
        ToLessonId = toLesson;
        
        RoadmapId = roadmapId;
    }
}