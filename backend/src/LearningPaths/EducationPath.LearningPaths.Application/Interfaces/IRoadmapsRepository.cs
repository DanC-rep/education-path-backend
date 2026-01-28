using EducationPath.LearningPaths.Domain.Entities;

namespace EducationPath.LearningPaths.Application.Interfaces;

public interface IRoadmapsRepository
{
    Task Add(Roadmap roadmap, CancellationToken cancellationToken = default);
}