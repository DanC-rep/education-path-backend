using EducationPath.LearningPaths.Domain.Entities;

namespace EducationPath.LearningPaths.Application.Interfaces;

public interface ILessonsRepository
{
    Task Add(Lesson lesson, CancellationToken cancellationToken = default);
}