using EducationPath.LearningPaths.Domain.Entities;

namespace EducationPath.LearningPaths.Application.Interfaces;

public interface ILessonsDependenciesRepository
{
    Task AddRange(IEnumerable<LessonDependency> dependencies, CancellationToken cancellationToken);
}