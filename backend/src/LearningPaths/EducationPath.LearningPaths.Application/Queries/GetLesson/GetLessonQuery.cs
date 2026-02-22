using EducationPath.Core.Abstractions;

namespace EducationPath.LearningPaths.Application.Queries.GetLesson;

public record GetLessonQuery(Guid LessonId) : IQuery;